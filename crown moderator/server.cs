$Pref::Server::Moderator::SuperAdminOnly = 1; 		 // Set to 0 to allow admins to set and remove moderators.
$Pref::Server::Moderator::MaxBanLength = 1440;		 // Maximum amount of time in minutes a moderator may ban someone, -1 for no limit
$Pref::Server::Moderator::AnnounceAutoModerator = 1; // Should "Player has become Moderator (Auto)" be displayed when they join?
$Pref::Server::Moderator::CanViewBanList = 0;	  	 // Should moderators be allowed to view the ban list? Still may not unban if set to 1.

exec("./lists.cs");

function isInt(%string) {
   return (%string $= mFloatLength(%string, 0));
}

function removeModerator(%blid)
{
	if(hasItemOnList($Pref::Server::AutoModeratorList, %blid))
	{
		$Pref::Server::AutoModeratorList = removeItemFromList($Pref::Server::AutoModeratorList, %blid);
		%victim = findClientByBL_ID(%blid);
		if(isObject(%victim))
		{
			commandToClient(%victim, 'setAdminLevel', 0);
			%victim.isModerator = 0;
		}
	}
	export("$Pref::Server::AutoModeratorList","config/server/prefs.cs");

}

function addModerator(%blid)
{
	if(!hasItemOnList($Pref::Server::AutoModeratorList, %blid))
	{
		$Pref::Server::AutoModeratorList = addItemToList($Pref::Server::AutoModeratorList, %blid);
		%victim = findClientByBL_ID(%blid);
		if(isObject(%victim))
		{
			commandToClient(%victim, 'setAdminLevel', 1);
			%victim.isModerator = 1;
		}
	}
	export("$Pref::Server::AutoModeratorList","config/server/prefs.cs");
}

package Moderator
{
	function gameConnection::autoAdminCheck(%this)
	{
		parent::autoAdminCheck(%this);
		if(hasItemOnList($Pref::Server::AutoModeratorList, %this.getBLID()))
		{
			commandToClient(%this, 'setAdminLevel', 1);
			%this.isModerator = 1;
			if($Pref::Server::Moderator::AnnounceAutoModerator)
				schedule(200, 0, "announce", "\c2"  @ %this.getPlayerName() SPC "has become Moderator (Auto)");
		}
		
		if(%this.isAdmin && %this.isModerator)
		{
			$Pref::Server::AutoModeratorList = removeItemFromList($Pref::Server::AutoModeratorList, %blid);
			%this.isModerator = 0;
			return;
		}

	}
	function serverCmdBan(%cl, %a, %target, %time, %reason) 
	{
		if(%cl.isAdmin)
		{
			parent::serverCmdBan(%cl, %a, %target, %time, %reason);
			return;
		}
		%victim = findClientByName(%target.bl_id);
		if(%cl.isModerator) 
		{
			if(%victim.isAdmin || %victim.isModerator || hasItemOnList($Pref::Server::AutoAdminList, %target) || hasItemOnList($Pref::Server::AutoSuperAdminList, %target))
			{
				commandToClient(%cl, 'messageBoxOK', "Oops", "You may not ban other staff members.");
				return;
			}
			
			if(%time > $Pref::Server::Moderator::MaxBanLength || %time == -1 && $Pref::Server::Moderator::MaxBanLength != -1)
			{
				commandToClient(%cl, 'messageBoxOK', "Oops", "You may only ban players for up to" SPC $Pref::Server::Moderator::MaxBanLength SPC "minutes.");
				return;
			}
			
			if(%cl.getBLID() $= %target)
			{
				commandToClient(%cl, 'messageBoxOK', "Nice try", "Go seek attention somewhere else.");
				return;
			}
			
			%cl.isAdmin = 1;
			parent::serverCmdBan(%cl, %a, %target, %time, %reason);
			%cl.isAdmin = 0;
			return;
		}
		parent::serverCmdBan(%cl, %a, %target, %time, %reason);
	}
	
	function serverCmdKick(%cl, %id) 
	{
		if(%id $= "")
			return;
		
		if(%cl.isModerator) 
		{
			if(%id.isAdmin || %id.isModerator)
			{
				commandToClient(%cl, 'messageBoxOK', "Oops", "You may not kick other staff members.");
				return;
			}
			%cl.isAdmin = 1;
			parent::serverCmdKick(%cl, %id);
			%cl.isAdmin = 0;
			return;
		}
		parent::serverCmdKick(%cl, %id);
	}
	function serverCmdRequestBanList(%cl) {
        if(%cl.isModerator && !%cl.isAdmin) 
		{
			if($Pref::Server::Moderator::CanViewBanList)
			{
				%cl.isAdmin = 1;
				parent::serverCmdRequestBanList(%cl);
				%cl.isAdmin = 0;
			}
			else
			{
				commandToClient(%cl,'messageBoxOK',"Shucks" ,"You may not view the ban list.");
				return;
			}
		}
		return parent::serverCmdRequestBanList(%cl);
    }
	
	function serverCmdUnBan(%cl, %idx)
	{
		if(%cl.isModerator && !%cl.isAdmin)
		{
			commandToClient(%cl,'messageBoxOK',"Shucks" ,"You may not unban players from the server, if you need a player unbanned please contact an administrator.");
			return;
		}
		parent::serverCmdUnBan(%cl, %idx);
	}
	
};
activatePackage(Moderator);

function serverCmdtoggleModerator(%cl, %target)
{
	if(!%cl.isAdmin)
	{
		messageClient(%cl, '', "\c6You must be an admin to set moderators.");
		return;
	}
	if($Pref::Server::Moderator::SuperAdminOnly == 1 && !%cl.isSuperAdmin)
	{
		messageClient(%cl, '', "\c6You must be a super admin to set moderators.");
		return;
	}
	if(%target $= "")
	{
		messageClient(%cl, '', "\c6You must input a a target name or BL_ID.");
		return;
	}
	%victim = findClientByName(%target);
	if(!isObject(%victim))
		%victim = findClientByBL_ID(%target);
	
	if(isObject(%victim))
	{
		if(%victim.isAdmin)
		{
			messageClient(%cl, '', "\c6This player is admin or higher.");
			return;
		}
		%name = %victim.getPlayerName();
		%blid = %victim.getBLID();
	}
	else
	{
		if(!isInt(%target) || %target < 0)
		{
			messageClient(%cl, '', "\c6Unable to find player, please input a BL_ID or correctly type their name.");
			return;
		}
		%blid = %target;
	}
	
	
	if(hasItemOnList($Pref::Server::AutoModeratorList, %blid))
	{
		removeModerator(%blid);
		announce("\c2" @ %cl.getPlayerName() SPC "revoked moderator from" SPC %name @ "(BL_ID: " @ %blid @ ")");
		if(isObject(%victim))
		{
			commandToClient(%victim, 'setAdminLevel', 0);
			%victim.isModerator = 0;
		}
	}
	else
	{
		addModerator(%blid);
		announce("\c2" @ %cl.getPlayerName() SPC "granted moderator to" SPC %name @ "(BL_ID: " @ %blid @ ")");
		if(isObject(%victim))
		{
			commandToClient(%victim, 'setAdminLevel', 1);
			%victim.isModerator = 1;
		}
	}
}

function serverCmdListModerators(%cl)
{
	messageClient(%cl, '', "\c3Online Moderators:");
	for(%i=0; %i < clientGroup.getCount(); %i++)
	{
		%cls = clientGroup.getObject(%i);
		if(%cls.isModerator)
		{
			%count++;
			messageClient(%cl, '', "\c6" @ %cls.getPlayerName());
		}
	}
	if(%count $= "")
		messageClient(%cl, '', "\c6None");
}

function serverCmdListMods(%cl)
{
	serverCmdListModerators(%cl);
}

function serverCmdSetMod(%cl, %target)
{
	serverCmdtoggleModerator(%cl, %target);
}

function serverCmdunMod(%cl, %target)
{
	serverCmdtoggleModerator(%cl, %target);
}