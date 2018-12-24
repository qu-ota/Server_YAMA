//Ban command for moderators
//Originally created by Tezuni, plus a couple enhancements by Dominoes including reason input
if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	RTB_registerPref("Max Ban Length","YAMA | Ban","$Pref::Server::YAMA::MaxBanLength","int 60 10080","Server_YAMA",1440,0,0);
	echo("=== YAMA | Ban.cs | Preferences registered successfully. ===");
}
else
{
	$Pref::Server::YAMA::MaxBanLength = 1440;		 // Maximum amount of time in minutes a moderator may ban someone, -1 for no limit
	echo("=== YAMA | Ban.cs | Preferences manager not found, values for commands set ===");
}

function isInteger(%string) //determines if a number is an actual digit
{
	%search = "0 1 2 3 4 5 6 7 8 9";
	for(%i=0;%i<getWordCount(%search);%i++)
	{
		%string = strReplace(%string,getWord(%search,%i),"");
	}
	if(%string $= "")
		return true;
	return false;
}

function serverCmdB(%client,%player,%time,%r1,%r2,%r3,%r4,%r5,%r6,%r7,%r8,%r9,%r10,%r11,%r12,%r13,%r14,%r15,%r16,%r17,%r18,%r19,%r20)
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
		%reason = "You were banned by server staff for" SPC %time @ " minutes. | Issued by" SPC %client.name;
	}
	
	%player = findClientByName(%player);
	
	if(%time > $Pref::Server::YAMA::MaxBanLength)
	{
		messageClient(%client,'',"\c7You cannot ban someone for a time greater than \c6" @ $Pref::Server::YAMA::MaxBanLength SPC "\c7minutes.");
	}
	
	if(%client.isAdmin)
	{
		if(%player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c2You cannot ban other administrators with this command.");
         if(!isObject(%player))
        	return messageClient(%client, '', "\c2Player not found.");
 		if(%time=="" || %time<10)
			%time=10;
 		serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time, %reason);
		return;	
	}
	
	if(%client.isModerator)
	{
		if(%client.ModeratorCommandSpam + 5 > $Sim::Time)
			return;
		
		%client.ModeratorCommandSpam = $Sim::Time;
		
		if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c6You cannot ban other server staff with this command.");
        if(!isObject(%player))
        	return messageClient(%client, '', "\c2Player not found.");
        %client.isAdmin = 1;
        messageAll('MsgAdminForce', '\c2%1 banned %2 (ID: %3): %4', %Client.name, %Player.name, %Player.bl_id, %reason);
		if(%time=="" || %time<10)
			%time=10;
        serverCmdBan(%client,%player.getPlayerName(), %player.BL_ID, %time, %reason);
 		%client.isAdmin = 0;
		return;	
	}
	messageClient(%client, '', "\c3/B \c2is for server staff only (\c3Moderator+\c2).");
}

$YAMA::Ban::Name = "ban.cs (Ban Script)";
$YAMA::Ban::Author = "Tezuni";
$YAMA::Ban::Commands = "/b victim time multi-word reason";
$YAMA::Ban::Information = "Ban someone. Supports reason input.";