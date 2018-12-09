//Ban command for moderators
//Originally found in Crown's moderator, plus a couple enhancements by Dominoes

function serverCmdBan(%cl, %a, %target, %time, %reason) 
{
	if(%cl.isAdmin)
	{
		if(%reason=="")
			%reason = "You have been banned by server staff. | Issued by" SPC %client.name;
		else
			%reason =  %reason SPC "| Issued by" SPC %client.name;
		
		parent::serverCmdBan(%cl, %a, %target, %time, %reason);
		return;
	}
	%victim = findClientByName(%target.bl_id);
	if(%cl.isModerator) 
	{
		if(%victim.isAdmin || %victim.isModerator || hasItemOnList($Pref::Server::AutoAdminList, %target) || hasItemOnList($Pref::Server::AutoSuperAdminList, %target))
		{
			messageClient(%cl,'',"\c6You may not ban other staff members.");
			return;
		}
		
		if(%time > $Pref::Server::Moderator::MaxBanLength || %time == -1 && $Pref::Server::Moderator::MaxBanLength != -1)
		{
			messageClient(%cl,'', "\c7You may only ban players for up to\c6" SPC $Pref::Server::Moderator::MaxBanLength SPC "\c7minutes.");
			return;
		}
		
		if(%cl.getBLID() $= %target)
		{
			messageClient(%cl,'',"\c7Nice try, \c6" @ %client.name @ "\c7, but go seek attention somewhere else.");
			return;
		}
		
		if(%reason=="")
			%reason = "You have been banned by server staff. | Issued by" SPC %client.name;
		else
			%reason =  %reason SPC "| Issued by" SPC %client.name;
		
		serverCmdBan(%cl, %a, %target, %time, %reason);
		return;
	}
	if(%reason=="")
		%reason = "You have been banned by server staff. | Issued by" SPC %client.name;
	else
		%reason =  %reason SPC "| Issued by" SPC %client.name;
	
	parent::serverCmdBan(%cl, %a, %target, %time, %reason);
}
