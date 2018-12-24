//Kick command for moderators
//Originally created by Tezuni, edited by Dominoes to support reasons for kicking
function servercmdK(%client,%player,%r1,%r2,%r3,%r4,%r5,%r6,%r7,%r8,%r9,%r10,%r11,%r12,%r13,%r14,%r15,%r16,%r17,%r18,%r19,%r20)
{
	if(%r1 !$= "")
	{
		%reason = %r1 SPC %r2 SPC %r3 SPC %r4 SPC %r5 SPC %r6 SPC %r7 SPC %r8 SPC %r9 SPC %r10 SPC %r11 SPC %r12 SPC %r13 SPC %r14 SPC %r15 SPC %r16 SPC %r17 SPC %r18 SPC %r19 SPC %r20;
		%reason = strreplace(%reason,"_"," ");
		%reason = stripTrailingSpaces(%reason);
		%reason = %reason SPC "| Issued by" SPC %client.name;
	}
	else
	{
		%reason = "You were kicked by server staff. | Issued by" SPC %client.name;
	}
	
	%player = findClientByName(%player);
	
	if(%client.isAdmin)
	{
		if(%player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c2You cannot kick other administrators with this command.");
		if(!isObject(%player))
			return messageClient(%client, '', "\c2Player not found.");
		messageAll('MsgAdminForce', '\c2%1 kicked %2 (ID: %3): %4', %client.name, %player.name, %player.bl_id, %reason);
		%player.delete("You have been kicked:" SPC %reason);
		return;	
	}
 	if(%client.isModerator)
	{
		if(%client.ModeratorCommandSpam + 5 > $Sim::Time)
			return;
		
		%client.ModeratorCommandSpam = $Sim::Time;
		
		if(%player.isModerator || %player.isAdmin)
			return messageClient(%client, '', "\c6You cannot kick other staff members with this command.");
		if(!isObject(%player))
			return messageClient(%client, '', "\c2Player not found.");
		messageAll('MsgAdminForce', '\c2%1 kicked %2 (ID: %3): %4', %client.name, %player.name, %player.bl_id, %reason);
		%player.delete("You have been kicked:" SPC %reason);
		return;	
	}
	messageClient(%client, '', "\c3/K \c2is for server staff only (\c3Moderator+\c2).");
}

$YAMA::Kick::Name = "kick.cs (Kick Script)";
$YAMA::Kick::Author = "Tezuni";
$YAMA::Kick::Commands = "/k victim multi-word reason";
$YAMA::Kick::Information = "Kick someone. Supports reason input.";