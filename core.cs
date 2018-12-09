//Main Moderator module
//Without this file being here, this addon won't load!
//Please, PLEASE don't touch this unless you're absolutely sure you know what you're doing.

$PrefManagerEnabled = if(isFile("Add-Ons/System_ReturnToBlockland/server.cs") || isFile("Add-Ons/System_BlocklandGlass/server.cs"))

if($PrefManagerEnabled = 1)
{
	if(!$RTB::RTBR_ServerControl_Hook)
	{
		RTB_registerPref("Super Admin Only","YAMA","$Pref::Server::Moderator::SuperAdminOnly","bool","Server_YAMA",1,0,0);
		RTB_registerPref("Max Ban Length","YAMA","$Pref::Server::Moderator::MaxBanLength","int 60 10080","Server_YAMA",1440,0,0);
		RTB_registerPref("Announce Auto Moderator?","YAMA","$Pref::Server::Moderator::AnnounceAutoModerator","bool","Server_YAMA",1,0,0);
		echo("=== YAMA | Preferences registered successfully. ===");
	}
}
else
{
	$Pref::Server::Moderator::SuperAdminOnly = 1; 		 // Set to 0 to allow admins to set and remove moderators.
	$Pref::Server::Moderator::MaxBanLength = 1440;		 // Maximum amount of time in minutes a moderator may ban someone, -1 for no limit
	$Pref::Server::Moderator::AnnounceAutoModerator = 1; // Should "Player has become Moderator (Auto)" be displayed when they join?
	echo("=== YAMA | Blockland Glass / Return to Blockland not found, values for commands set ===");
}

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
			commandToAll('Glass_setPlayerlistStatus', %victim, "-", 1);
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
			commandToAll('Glass_setPlayerlistStatus', %victim, "M", 1);
			%victim.isModerator = 1;
		}
	}
	export("$Pref::Server::AutoModeratorList","config/server/prefs.cs");
}

function gameConnection::autoAdminCheck(%this)
{
	parent::autoAdminCheck(%this);
	if(hasItemOnList($Pref::Server::AutoModeratorList, %this.getBLID()))
	{
		commandToAll('Glass_setPlayerlistStatus', %victim, "M", 1);
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
			%victim.isModerator = 0;
	}
	else
	{
		addModerator(%blid);
		announce("\c2" @ %cl.getPlayerName() SPC "granted moderator to" SPC %name @ "(BL_ID: " @ %blid @ ")");
		if(isObject(%victim))
			%victim.isModerator = 1;
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
