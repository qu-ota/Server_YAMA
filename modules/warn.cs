//Warning addon
//By Tezuni & DAProgs

function servercmdw(%client,%target, %parm1, %parm2, %parm3, %parm4, %parm5, %parm6, %parm7, %parm8, %parm9, %parm10, %parm11, %parm12, %parm13, %parm14, %parm15, %parm16, %parm17, %parm18, %parm19, %parm20)
{
	servercmdwarn(%client,%target, %parm1, %parm2, %parm3, %parm4, %parm5, %parm6, %parm7, %parm8, %parm9, %parm10, %parm11, %parm12, %parm13, %parm14, %parm15, %parm16, %parm17, %parm18, %parm19, %parm20);
}

function servercmdwarn(%client,%target, %parm1, %parm2, %parm3, %parm4, %parm5, %parm6, %parm7, %parm8, %parm9, %parm10, %parm11, %parm12, %parm13, %parm14, %parm15, %parm16, %parm17, %parm18, %parm19, %parm20)
{
	%reason = %parm1 SPC %parm2 SPC %parm3 SPC %parm4 SPC %parm5 SPC %parm6 SPC %parm7 SPC %parm8 SPC %parm9 SPC %parm10 SPC %parm11 SPC %parm12 SPC %parm13 SPC %parm14 SPC %parm15 SPC %parm16 SPC %parm17 SPC %parm18 SPC %parm19 SPC %parm20; 
	//echo("T1:"@%reason);
	%Status = %client.isSuperAdmin + %client.isAdmin;
	%player = findClientByName(%target);
//	%target = findClientByName(%user).player;

	if(!%client.isModerator && !%client.isAdmin)
		return messageClient(%client, '', "\c6Only server staff may use this command.");

	if(!isObject(%player))
		return messageClient(%client, '', "\c5Player not found.");

		if(%client.isModerator)
		{
			if(%client.ModeratorCommandSpam + 5 > $Sim::Time)
				return;
		
			%client.ModeratorCommandSpam = $Sim::Time;
			
			if(%target.isModerator || %target.isAdmin)
				return messageClient(%client, '', "\c5You cannot warn server staff.");
		}

		if(%client.isAdmin && !%client.bl_id == getNumKeyID())
		{
			if(%target.isAdmin)
			{
				return messageClient(%client, '', "\c5You cannot warn fellow admins.");
			}
		}

		if(!%parm1$="")
			echo("C1:"@%parm1);

		if(%reason $= "")
		{
			messageClient(%client, '', "\c5Please specify a reason after the name.");
			return;
		}
		if(%target.isAdmin)
		{
			return;
		}

		messageAll('',"Staff Announcement:" SPC %player.name SPC "was warned by" SPC %client.name SPC "for:\c2" SPC %reason);

		CommandToClient(%player, 'MessageBoxOK', "Warning (Issued by" @ %client.name @ ")", "You were warned by a staff member.\nReason: " @ %reason);
}
