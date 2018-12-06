//Ban command for moderators
//Originally created by Tezuni, plus a couple enhancements by Dominoes including reason input

function serverCmdB(%client,%player,%time,%c1,%c2,%c3,%c4,%c5,%c6,%c7,%c8,%c9,%c10,%c11,%c12,%c13,%c14,%c15,%c16,%c17,%c18,%c19,%c20)
{
	%player = findClientByName(%player);
	
	if(%c1 !$= "")
	{
		for(%a = 1; %a < 21; %a++)
		{
			if(%c[%a] !$= "")
			{
				%player.reason = %player.reason SPC %c[%a] SPC "| Issued by" SPC %client.name;
			}
		}
	}
	else
	{
		%player.reason = "You were banned by server staff. | Issued by" SPC %client.name;
	}
	
	if(%client.isAdmin)
	{
		if(%player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c2You cannot ban other administrators with this command.");

        if(!isObject(%player))
        	return messageClient(%client, '', "\c2Player \c3" @ %player.name @ "\c2 not found.");

		if(%time=="")
			%time=10;

		serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time, %player.reason SPC "| Issued by" SPC %client.name);
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
        serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time, %player.reason SPC "|Issued by" SPC %client.name);

		%client.isAdmin = 0;
		return;	
	}
	messageClient(%client, '', "\c3/K \c2is for server staff only (\c3Moderator+\c2).");
}
