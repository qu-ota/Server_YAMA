//Kick command for moderators
//Originally created by Crown, plus a couple enhancements by Dominoes including reason input

function serverCmdKick(%cl,%id,%c1,%c2,%c3,%c4,%c5,%c6,%c7,%c8,%c9,%c10,%c11,%c12,%c13,%c14,%c15,%c16,%c17,%c18,%c19,%c20) 
{
	if(%c1 !$= "")
	{
		for(%a = 1; %a < 21; %a++)
		{
			if(%c[%a] !$= "")
			{
				%id.reason = %id.reason SPC %c[%a] SPC "| Issued by" SPC %client.name;
			}
		}
	}
	else
	{
		%id.reason = "You were kicked by server staff. | Issued by" SPC %client.name;
	}
	
	if(%id $= "")
	{
		messageClient(%cl,'',"\c7Invalid syntax; probably a user that doesn't exist.");
		messageClient(%cl,'',"\c7Proper syntax: \c6/kick \c3name \c420-word reason");
		return;
	}
	
	if(%cl.isModerator) 
	{
		if(%id.isAdmin || %id.isModerator)
		{
			messageClient(%cl,'',"\c7You may not kick other staff members.");
			return;
		}
		messageAll('MsgAdminForce', '\c2[\c3Moderator\c2] \c3%1 \c2kicked \c3%2\c2(ID: %3)', %client.name, %player.name, %player.bl_id);
		%id.delete(%id.reason);
		return;
	}
	parent::serverCmdKick(%cl,%id,%c1,%c2,%c3,%c4,%c5,%c6,%c7,%c8,%c9,%c10,%c11,%c12,%c13,%c14,%c15,%c16,%c17,%c18,%c19,%c20);
}
