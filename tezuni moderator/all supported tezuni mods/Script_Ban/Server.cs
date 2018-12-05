//////////////////
//Ban Command/////
//////////////////
//Author: Tezuni//
//////////////////

function servercmdb(%client,%Player,%time)
{
	%player = findClientByName(%player);
	
	if(%client.isAdmin)
	{
		if(%player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c2You cannot ban other admins with this command.");

        if(!isObject(%player))
        	return messageClient(%client, '', "\c2Player not found.");

		if(%time=="")
			%time=10;

		serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time,"You were banned by server staff for" SPC %time SPC "minutes.");
		return;	
	}
	
	if(%client.isModerator)
	{
		if(%client.ModeratorCommandSpam + 5 > $Sim::Time)
			return;
		
		%client.ModeratorCommandSpam = $Sim::Time;
		
		if(%player.isModerator || %player.isAdmin)
			return messageClient(%client, '', "\c6You cannot ban other server staff with this command.");

        if(!isObject(%player))
        	return messageClient(%client, '', "\c6Player not found."); 

        %client.isAdmin = 1;
        messageAll('MsgAdminForce', '\c6  ==> [\c4MOD\c6] \c4%1 \c6banned \c4%2\c2(ID: %3)', %Client.name, %Player.name, %Player.bl_id);
		
		%time=10;
        serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time,"You were banned by server staff for" SPC %time SPC "minutes.");

		%client.isAdmin = 0;
		return;	
	}
	messageClient(%client, '', "\c6Only server staff may use this command.");
}
