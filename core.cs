//Main Moderator module
//Without this file being here, this addon won't load!
//Please, PLEASE don't touch this unless you're absolutely sure you know what you're doing.

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	RTB_registerPref("Super Admin Only","YAMA","$Pref::Server::YAMA::SuperAdminOnly","bool","Server_YAMA",1,0,0);
	RTB_registerPref("Announce Auto Moderator?","YAMA","$Pref::Server::YAMA::AnnounceAutoModerator","bool","Server_YAMA",1,0,0);
	echo("=== YAMA | Preferences registered successfully. ===");
}
else
{
	$Pref::Server::YAMA::SuperAdminOnly = 1; 		 // Set to 0 to allow admins to set and remove moderators.
	$Pref::Server::YAMA::AnnounceAutoModerator = 1; // Should "Player has become Moderator (Auto)" be displayed when they join?
	echo("=== YAMA | Preferences manager not found, values for commands set ===");
}

function YAMA_addAutoStatus(%blid)
{
	$Pref::Server::AutoModeratorList = addItemToList($Pref::Server::AutoModeratorList,%blid);
	export("$Pref::Server::*","config/server/prefs.cs");
}

function YAMA_removeAutoStatus(%blid)
{
	$Pref::Server::AutoModeratorList = removeItemFromList($Pref::Server::AutoModeratorList,%blid);
	export("$Pref::Server::*","config/server/prefs.cs");
}

function GameConnection::autoAdminCheck(%this)
{
	schedule(100, 0, checkYAMAmods, %this);
	return Parent::autoAdminCheck(%this);
}

function checkYAMAmods(%client)
{
	if(hasItemOnList($Pref::Server::AutoModeratorList,%client.bl_id))
	{
		%client.isModerator = true;
		%client.isAutoModerator = true;
		messageAll('MsgAdminForce', '\c4%1 has become Moderator (Auto)', %client.name);
		commandToAll('Glass_setPlayerListStatus', %client.bl_id, "M", 1);
	}
}

function serverCmdMod(%client, %player, %type) 
{
	if(!%client.isSuperAdmin && $Pref::Server::YAMA::SuperAdminOnly == 1)
		return messageClient(%client, '', "\c6Only \c2super admins \c6may add \c2moderators\c6.");
	else
	{
		if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c2admins \c6may add \c2moderators\c6.");
	}
	
	%player = findClientByName(%player);
	
	if(%player.isAdmin || %player.isSuperAdmin)
		return messageClient(%client,'',"\c6" @ %player.name SPC "is already a staff member. If they're a moderator, try /demod.");
	
	%blid = %player.getBLID();
	
	if(%type $= "auto" || %type $= "a")
	{
		if((%player.isModerator && !%player.isAutoModerator) || !%player.isModerator)
		{
			messageAll('MsgAdminForce', '\c2%1 has become Moderator (Auto)', %player.name);
			schedule(100,0,YAMA_addAutoStatus,%blid);
			%player.isModerator = true;
			%player.isAutoModerator = true;
			commandToAll('Glass_setPlayerListStatus', %player.bl_id, "M", 1);
		}
		else
			return;
	}
	else
	{
		if(!%player.isModerator)
		{
			messageAll('MsgAdminForce', '\c2%1 has become Moderator (Manual)', %player.name);
			%player.isModerator = true;
			if(isObject(%player))
				commandToAll('Glass_setPlayerListStatus', %player.bl_id, "M", 1);
		}
		else
			return;
	}
}

function serverCmdDeMod(%client, %player)
{
	if(!%client.isSuperAdmin && $Pref::Server::YAMA::SuperAdminOnly == 1)
		return messageClient(%client, '', "\c6Only \c2super admins \c6may remove \c2moderators\c6.");
	else
	{
		if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c2admins \c6may remove \c2moderators\c6.");
	}
	
	%player = findClientByName(%player);
	
	if(!%player.isModerator)
		return messageClient(%client,'',"\c6" @ %player.name SPC "is not a \c4moderator\c6.");
	
	%blid = %player.getBLID();
	
	messageAll('MsgAdminForce', '\c2%1 has been demoted from moderator (Auto & Manual)', %player.name);
	schedule(100,0,YAMA_removeAutoStatus,%blid);
	%player.isModerator = false;
	if(%player.isAutoModerator)
		%player.isAutoModerator = false;
	commandToAll('Glass_setPlayerListStatus', %player.bl_id, "-", 1);
}

function serverCmdListModerators(%client)
{
	if(%client.isModerator || %client.isAdmin || %client.isSuperAdmin)
		listModerators(%client);
	
	else
		messageClient(%client,'',"\c6You must be part of server staff to use this command.");
}

function serverCmdListMods(%client)
{
	serverCmdListModerators(%client);
}

function listModerators(%client)
{
	messageClient(%client,'',"\c6Online Moderators:");
	
		for(%i=0; %i < ClientGroup.getCount(); %i++)
		{
			%playerget = ClientGroup.getObject(%i);
			
			if(%playerget.isModerator)
			{
				messageClient(%client,'',"\c2" @ %playerget.name);
			}
		}
}

//special stuff like orbing and finding (fetching is abusive)

package YAMA_modPerms
{
function serverCmdDropCameraAtPlayer(%cl)
{
	if(%cl.isModerator)
	{
		%cl.isAdmin = 1;
		parent::serverCmdDropCameraAtPlayer(%cl);
		%cl.isAdmin = 0;
	}
	if(%cl.isAdmin)
		parent::serverCmdDropCameraAtPlayer(%cl);
}

function serverCmdDropPlayerAtCamera(%cl)
{
	if(%cl.isModerator)
	{
		%cl.isAdmin = 1;
		parent::serverCmdDropPlayerAtCamera(%cl);
		%cl.isAdmin = 0;
	}
	if(%cl.isAdmin)
		parent::serverCmdDropPlayerAtCamera(%cl);
}

function serverCmdWarp(%cl)
{
	if(%cl.isModerator)
	{
		%cl.isAdmin = 1;
		parent::serverCmdWarp(%cl);
		%cl.isAdmin = 0;
	}
	if(%cl.isAdmin)
		parent::serverCmdWarp(%cl);
}

function serverCmdFetch(%cl, %target)
{
	if(%cl.isModerator)
	{
		%this = findClientByName(%target);
		if(!isObject(%this))
			return;
		
		%cl.isAdmin = 1;
		parent::serverCmdFetch(%cl, %target);
		%cl.isAdmin = 0;
		messageAll('MsgAdminForce',"\c3" @ %cl.name SPC "\c6has fetched \c3" @ %this.name @ "\c6.");
	}
	if(%cl.isAdmin)
	{
		%this = findClientByName(%target);
		if(!isObject(%this))
			return;
		
		parent::serverCmdFetch(%cl, %target);
		messageAll('MsgAdminForce',"\c3" @ %cl.name SPC "\c6has fetched \c3" @ %this.name @ "\c6.");
	}
}

function serverCmdFind(%cl, %target)
{
	if(%cl.isModerator)
	{
		%this = findClientByName(%target);
		if(!isObject(%this))
			return;
		
		%cl.isAdmin = 1;
		parent::serverCmdFind(%cl, %target);
		%cl.isAdmin = 0;
		messageAll('MsgAdminForce',"\c3" @ %cl.name SPC "\c6went to \c3" @ %this.name @ "\c6.");
	}
	if(%cl.isAdmin)
	{
		%this = findClientByName(%target);
		if(!isObject(%this))
			return;
		
		parent::serverCmdFind(%cl, %target);
		messageAll('MsgAdminForce',"\c3" @ %cl.name SPC "\c6went to \c3" @ %this.name @ "\c6.");
	}
}

function serverCmdSpy(%cl, %target)
{
	if(%cl.isModerator)
	{
		%this = findClientByName(%target);
		if(!isObject(%this))
			return;
		
		%cl.isAdmin = 1;
		parent::serverCmdSpy(%cl, %target);
		%cl.isAdmin = 0;
	}
	if(%cl.isAdmin)
		parent::serverCmdSpy(%cl, %target);
}
};
activatePackage(YAMA_modPerms);
