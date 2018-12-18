//Main Moderator module
//Without this file being here, this addon won't load!
//Please, PLEASE don't touch this unless you're absolutely sure you know what you're doing.

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs") || isFile("Add-Ons/System_BlocklandGlass/server.cs"))
{
	RTB_registerPref("Super Admin Only","YAMA","$Pref::Server::YAMA::SuperAdminOnly","bool","Server_YAMA",1,0,0);
	RTB_registerPref("Max Ban Length","YAMA","$Pref::Server::YAMA::MaxBanLength","int 60 10080","Server_YAMA",1440,0,0);
	RTB_registerPref("Announce Auto Moderator?","YAMA","$Pref::Server::YAMA::AnnounceAutoModerator","bool","Server_YAMA",1,0,0);
	RTB_registerPref("Show Mod Shields?","YAMA","$Pref::Server::YAMA::ShowModShields","bool","Server_YAMA",1,0,0);
	echo("=== YAMA | Preferences registered successfully. ===");
}
else
{
	$Pref::Server::YAMA::SuperAdminOnly = 1; 		 // Set to 0 to allow admins to set and remove moderators.
	$Pref::Server::YAMA::MaxBanLength = 1440;		 // Maximum amount of time in minutes a moderator may ban someone, -1 for no limit
	$Pref::Server::YAMA::AnnounceAutoModerator = 1; // Should "Player has become Moderator (Auto)" be displayed when they join?
	$Pref::Server::YAMA::ShowModShields = 1;
	echo("=== YAMA | Blockland Glass / Return to Blockland not found, values for commands set ===");
}

function Mod_addAutoStatus(%client, %player)
{
    $Pref::Server::AutoModList = addItemToList($Pref::Server::AutoModeratorList,%player.bl_id);
    export("$Pref::Server::AutoModList","config/server/prefs.cs");
}

function Mod_removeAutoStatus(%client, %player)
{
	$Pref::Server::AutoModList = removeItemFromList($Pref::Server::AutoModeratorList,%player.bl_id);
	export("$Pref::Server::*","config/server/prefs.cs");
}

function checkmod(%client)
{
    %list = $Pref::Server::AutoModList;
    %bl_id = %client.bl_id;
	if(hasItemOnList(%list,%bl_id))
	{
		%client.isModerator = true;
		%client.isAutoModerator = true;
		commandToAll('Glass_setPlayerListStatus', %player.bl_id, "M", 1);
		messageAll('MsgAdminForce', '\c4%1 has become Moderator (Auto)', %client.name);
	}
}  

function serverCmdMod(%client, %player, %type) 
{
	if(!%client.isSuperAdmin && $Pref::Server::YAMA::SuperAdminOnly == 1)
		return messageClient(%client, '', "\c6Only \c3super admins \c6may add \c4moderators\c6.");
	else
	{
		if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c3admins \c6may add \c4moderators\c6.");
	}
	
    %player = findClientByName(%player);
      
	if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)                                  
		return messageClient(%client,'',"\c6" @ %player.name SPC "is already a staff member. If they're a moderator, try /demod.");
	
	if(%type $="auto" || %type $="a")
	{
		if(%player.isAutoModerator == false)
			return messageClient(%client,'',"\c6" @ %player.name SPC "is already an auto moderator. Try /demod if this wasn't what you're looking for.");
		messageAll('MsgAdminForce', '\c2%1 has become Moderator (Auto)', %player.name);
		schedule(100, 0, Mod_addAutoStatus, %player);
		%player.isAutoModerator = true;
	}
	else
		messageAll('MsgAdminForce', '\c2%1 has become Moderator (Manual)', %player.name);
	%player.isModerator = true;
	if(isObject(%player))
		commandToAll('Glass_setPlayerListStatus', %player.bl_id, "M", 1);
}

function serverCmdDeMod(%client, %player)
{       
	if(!%client.isSuperAdmin && $Pref::Server::YAMA::SuperAdminOnly == 1)
		return messageClient(%client, '', "\c6Only \c3super admins \c6may remove \c4moderators\c6.");
	else
	{
		if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c3admins \c6may remove \c4moderators\c6.");
	}
	
	%player = findClientByName(%player);
	
	if(!%player.isModerator)              
		return messageClient(%client,'',"\c6" @ %player.name SPC "is not a \c4moderator\c6.");
	
	messageAll('MsgAdminForce', '\c2%1 has been demoted from moderator (Auto)', %player.name);
	schedule(100, 0, Mod_RemoveAutoStatus, %player);
	%player.isModerator = false;
	if(%player.isAutoModerator)
		%player.isAutoModerator = false;
	if(isObject(%player))
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
			%target = ClientGroup.getObject(%i);
               
			if(%target.isModerator)
			{
				messageClient(%client,'',"\c2" @ %target.name);
			}
		}
}