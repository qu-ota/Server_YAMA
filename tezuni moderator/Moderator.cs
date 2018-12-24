$System_Cloud=0;

package PlayerModerator
{
        function GameConnection::autoAdminCheck(%this)
        {
				if($System_Cloud == 1)
					return Parent::autoAdminCheck(%this);
					
					schedule(100, 0, checkmod, %this);
					schedule(100, 0, checkreg, %this);
					return Parent::autoAdminCheck(%this);
        }
};
ActivatePackage(PlayerModerator);

//moderator
function serverCmdMod(%client, %player) 
{
        if(!%client.isSuperAdmin)
			return messageClient(%client, '', "\c6Only \c0super admins \c6may grant \c4moderator\c6.");

        %player = findClientByName(%player);
 
        if(!%player.isREG)
			return messageClient(%client, '', "\c6Only \c1regulars \c6may become \c4moderators\c6.");
       
        if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)                                  
			return messageClient(%client,'',"\c6" @ %player.name SPC "is already a staff member.");
       
		if($System_Cloud == 1)
		{
			%player.isModerator = true;
			messageAll('MsgAdminForce', '\c4%1 has become Moderator (Cloud)', %player.name);
		}
		else
		{
			messageAll('MsgAdminForce', '\c4%1 has become Moderator (Auto)', %player.name);
			schedule(100, 0, Mod_addAutoStatus, %player);
			%player.isModerator = true;
			schedule(100, 0, REG_removeAutoStatus, %player);
			%player.isREG = false;			
		}
       
}
 
function serverCmdDeMod(%client, %player)
{       
        if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c3admins \c6may revoke \c4moderator\c6.");
 
        %player = findClientByName(%player);
 
        if(!%player.isModerator)              
			return messageClient(%client,'',"\c6" @ %player.name SPC "is not a \c4moderator\c6.");

		if($System_Cloud == 1)
		{
			%player.isModerator = false;
			%player.isREG = false;
			messageAll('MsgAdminForce', '\c4%1 has been demoted from moderator (Cloud)', %player.name);
			//parent this command in your cloud add-on to make it a permanent rank
		}
		else
		{
			messageAll('MsgAdminForce', '\c4%1 has been demoted from moderator (Auto)', %player.name);
			schedule(100, 0, Mod_RemoveAutoStatus, %player);
			%player.isModerator = false;
		}
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
                    messageClient(%client,'',"\c4" @ %target.name);
                }
           }
}
 
function checkmod(%client)
{
        %list = $Pref::Server::AutoModList;
        %bl_id = %client.bl_id;
        if(hasItemOnList(%list,%bl_id))
        {
            %client.isModerator = true;
            messageAll('MsgAdminForce', '\c4%1 has become Moderator (Auto)', %client.name);
        }
}  
 
//- serverCmdRTB_addAutoStatus (Allows a client to add a player to the auto list) --- Edited for Moderator
function Mod_addAutoStatus(%client)
{
    $Pref::Server::AutoModList = addItemToList($Pref::Server::AutoModList,%client.bl_id);
    export("$Pref::Server::*","config/server/prefs.cs");   
}

//- serverCmdRTB_removeAutoStatus (Removes a player from the auto lists) --- Edited for Moderator
function Mod_removeAutoStatus(%client)
{
	$Pref::Server::AutoModList = removeItemFromList($Pref::Server::AutoModList,%client.bl_id);
	export("$Pref::Server::*","config/server/prefs.cs");
}

//regular
function serverCmdReg(%client, %player) 
{
        if(!%client.isAdmin)
            return messageClient(%client, '', "\c6Only \c3admins \c6may grant \c1regular\c6.");

        %player = findClientByName(%player);
       
        if(%player.isReg)                                  
			return messageClient(%client,'',"\c6" @ %player.name SPC "is already a \c1regular\c6.");
       
		if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)                                  
			return messageClient(%client,'',"\c4" @ %player.name @ " \c2is already a staff member.");
			
		if($System_Cloud == 1)
		{
			%player.isReg = true;
			messageAll('MsgAdminForce', '<color:00B2EE>%1 has become Regular (Cloud)', %player.name);
			//parent this command in your cloud add-on to make it a permanent rank
		}
		else
		{
			messageAll('MsgAdminForce', '<color:00B2EE>%1 has become Regular (Auto)', %player.name);
			schedule(100, 0, reg_addAutoStatus, %player);
			%player.isReg = true;
		}
       
}
 
function serverCmdDeReg(%client, %player)
{       
        if(!%client.isAdmin)
			return messageClient(%client, '', "\c6Only \c3admins \c6may revoke \c1regular\c6.");
 
        %player = findClientByName(%player);
 
        if(!%player.isReg)              
			return messageClient(%client,'',"\c6" @ %player.name SPC "is not a \c1regular\c6.");

		if($System_Cloud == 1)
		{
			%player.isReg = false;
			messageAll('MsgAdminForce', '<color:00B2EE>%1 has been demoted from Regular (Cloud)', %player.name);
			//parent this command in your cloud add-on to make it a permanent rank
		}
		else
		{
			messageAll('MsgAdminForce', '<color:00B2EE>%1 has been demoted from Regular (Auto)', %player.name);	
			schedule(100, 0, reg_RemoveAutoStatus, %player);
			%player.isReg = false;
		}
}
  
function serverCmdListRegulars(%client)
{
	if(%client.isReg || %client.isAdmin || %client.isSuperAdmin)
		listRegulars(%client);
 
	else
		return messageClient(%client, '', "\c6Only \c3admins \c6may check for \c1regulars\c6.");
}
 
function listRegulars(%client)
{
	messageClient(%client,'',"\c6Online Regulars:");
        
    for(%i=0; %i < ClientGroup.getCount(); %i++)  
    {
		%target = ClientGroup.getObject(%i);
        if(%target.isReg)
		messageClient(%client,'',"\c1" @ %target.name);
    }
}

function serverCmdListRegs(%client)
{
        serverCmdListRegulars(%client);
}
 
function checkreg(%client)
{
        %list = $Pref::Server::AutoRegList;
        %bl_id = %client.bl_id;
		
        if(hasItemOnList(%list,%bl_id))
        {
                %client.isReg = true;
                messageAll('', '\c1%1 has become Regular (Auto)', %client.name);
        }
}  
 
//- serverCmdRTB_addAutoStatus (Allows a client to add a player to the auto list) --- Edited for Regular
function reg_addAutoStatus(%client)
{
	$Pref::Server::AutoRegList = addItemToList($Pref::Server::AutoRegList,%client.bl_id);
	export("$Pref::Server::*","config/server/prefs.cs");      
}


//- serverCmdRTB_removeAutoStatus (Removes a player from the auto lists) --- Edited for Regular
function reg_removeAutoStatus(%client)
{
	$Pref::Server::AutoRegList = removeItemFromList($Pref::Server::AutoRegList,%client.bl_id);
	export("$Pref::Server::*","config/server/prefs.cs");
}