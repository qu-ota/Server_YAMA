//////////////////
//Force Kill//////
//////////////////
//Author: Tezuni//
//////////////////

function servercmdKill(%client,%Player)
{
	%player = findClientByName(%player);

	if(%player.bl_id == getNumKeyID())
		return messageClient(%client, '', "\c6You can't kill the host.");

	if(!%client.isModerator && !%client.isAdmin && !%client.isSuperAdmin)
		return messageClient(%client, '', "\c6You can't use \c0/kill\c6.");
	
	if(%client.isModerator)
	{
		if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c6Moderators cannot force kill server staff.");
	}
	

    if(!isObject(%player))
		return messageClient(%client, '', "\c6Player not found.");

	if(!isObject(%player.player))
		return messageClient(%client,"","\c4"@ %player.name @" \c6is already dead.");
    
    %player.player.kill("suicide");
	StaffAnnouncement("Staff Announcement:" SPC %player.name SPC "was \c0force-killed\c3 by" SPC %client.name);
}