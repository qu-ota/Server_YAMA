//Mute command
//Super special file from Trinko
//Commands are in description.txt

if(!strLen($Pref::Server::Mute::PermaMutes) || $Pref::Server::Mute::PermaMutes)
	$Pref::Server::Mute::PermaMutes = 0;

if(!strLen($Pref::Server::Mute::MuteRestartonDC) || $Pref::Server::Mute::MuteRestartonDC)
	$Pref::Server::Mute::MuteRestartonDC = 0;
function getMuteTime(%t)
{
	//converts seconds to milliseconds, then converts it to other units of time
	%t *= 1000;
	%weeks = %t / (604800000 | 0) | 0;
	%t = %t % (604800000 | 0);
	%days = %t / (86400000 | 0) | 0;
	%t = %t % (86400000 | 0);
        %hours = %t/(3600000|0) | 0;
        %t = %t % (3600000|0);
        %minutes  = %t/60000 | 0;
        %t = %t % 60000;
        %seconds = %t / 1000 | 0;//%t - (%minutes * 60) | 0;
	//if( %seconds < 10 )
	//	%seconds = "0" @ %seconds;
	if(%weeks)
		%final = "\c3" @ %weeks SPC "week" @ (%weeks <= 1 ? "" : "s");
	if(%days)
		%final = %final SPC %days SPC "day" @ (%days <= 1 ? "" : "s");
	if(%hours)
		%final = %final SPC %hours SPC "hour" @ (%hours <= 1 ? "" : "s");
	if(%minutes)
		%final = %final SPC %minutes SPC "minute" @ (%minutes <= 1 ? "" : "s");
	if(%seconds)
		%final = (strLen(%final) ? %final SPC "\c2and\c3" SPC "" : "") @ %seconds SPC "second" @ (%seconds <= 1 ? "" : "s");
	//%final = (%weeks != 0 ? %weeks @ "\t" : "") @ (%days != 0 ? %days @ "\t" : "") @ (%hours != 0 ? %hours @ "\t" : "") @ (%minutes != 0 ? %minutes @ "\t" : "") @ %seconds;//%hours TAB %minutes TAB %seconds;
	//talk("week:" SPC %weeks); talk("day:" SPC %days); talk("hour:" SPC %hours); talk("minute:" SPC %minutes); talk("second:" SPC %seconds);
	return lTrim(%final);
}

package Mute
{
	function serverCMDMute(%c, %v, %t)
	{
		if(!%c.isMod || !%c.isModerator)
			return;
		if(!%c.isAdmin)
			return;
		if(!%tar = findClientbyName(%v))
			return;
		if(%tar == %c)
		{
			messageClient(%c, 'MsgClearBricks', "\c6Are you seriously muting \c0yourself\c6? You must be nuts.");
			return;
		}
		if($Server::Muted[%tar.getBLID()])
		{
			messageClient(%c, '', "\c6This client is already muted!");
			return;
		}
		if(%t $= "" || %v $= "")
			return;
		if(%c.getBLID() != getNumKeyID() || !%c.isCoHost)
		{
			if(%c.isSuperAdmin && %tar.isSuperAdmin)
			{
				messageClient(%c, '', "\c5You cannot mute other Super Admins.");
				return;
			}
			else if(%c.isAdmin && %tar.isSuperAdmin)
			{
				messageClient(%c, '', "\c5You cannot mute Super Admins.");
				return;
			}
			else if((%c.isAdmin && !%c.isSuperAdmin) && %tar.isAdmin)
			{
				messageClient(%c, '', "\c5You cannot mute other admins.");
				return;
			}
			if((%c.isSuperAdmin || %c.isAdmin) && (%tar.getBLID() == getNumKeyID() || %tar.isCoHost))
			{
				messageClient(%c, 'MsgClearBricks', "\c5You are not allowed to mute the \c0Host\c6.");
				return;
			}
		}
		if(%t > 59)
			%tt = %t / 60;
		else if(%t <= -1)
			%t = $Pref::Server::Mute::PermaMutes ? -1 : 0;
		//else
		//	%tt = %t;
		if(!%t < 1 && %t >= 0)
		{
			$Server::MutedTime[%tar.getBLID()] = %t;
			$Server::Muted[%tar.getBLID()] = 1;
			cancel($Server::MuteSch[%tar.getBLID()]);
			%tar.MuteSch(%t);
			messageAllExcept(%tar, -1, 'MsgAdminForce', "\c3" @ %c.getPlayerName() SPC "\c2muted\c3" SPC %tar.getPlayerName() SPC "\c2for\c3" SPC getMuteTime(%t) @ "\c2.");
			messageClient(%tar, 'MsgAdminForce', "\c3You\c2 have been muted by\c3" SPC %c.getPlayerName() SPC "\c2for\c3" SPC getMuteTime(%t) @ "\c2.");
			//messageAllExcept(%tar, -1, 'MsgAdminForce', "\c3" @ %c.getPlayerName() SPC "\c2muted\c3" SPC %tar.getPlayerName() SPC "\c2for\c3" SPC %tt SPC(%t > 59 ? "minute" : "second") @ (%t <= 1 ? "\c2." : "s\c2."));
			//messageClient(%tar, 'MsgAdminForce', "\c3You\c2 have been muted by\c3" SPC %c.getPlayerName() SPC "\c2for\c3" SPC %tt SPC (%t > 59 ? "minute" : "second") @ (%t <= 1 ? "\c2." : "s\c2."));
		}
		else if(%t == -1 && $Pref::Server::Mute::PermaMutes)
		{
			$Server::MutedTime[%tar.getBLID()] = -1;
			$Server::Muted[%tar.getBLID()] = 1;
			messageAllExcept(%tar, -1, 'MsgAdminForce', "\c3" @ %c.getPlayerName() SPC "\c2permanently muted\c3" SPC %tar.getPlayerName() @ "\c2.");
			messageClient(%tar, 'MsgAdminForce', "\c3You\c2 have been permanently muted by\c3" SPC %c.getPlayerName() @ "\c2.");
		}
		else
		{
			messageClient(%c, '', "\c2You have not specified a valid time for this mute.");
			return;
		}
	}

	function GameConnection::MuteSch(%this, %t)
	{
		if(!isObject(%this))
			return;
		if(!$Pref::Server::Mute::MuteRestartonDC)
			$Server::MutedTime[%this.getBLID()] = %t;
		cancel($Server::MuteSch[%this.getBLID()]);
		if(%t <= 0)
		{
			$Server::Muted[%this.getBLID()] = 0;
			$Server::MutedTime[%this.getBLID()] = 0;
			messageClient(%this, 'MsgAdminForce', "\c2You are now unmuted.");
			return;
		}
		$Server::MuteSch[%this.getBLID()] = %this.scheduleNoQuota(1000, MuteSch, %t--);
	}

	function serverCMDunmute(%c, %v)
	{
		if(!%c.isModerator || !%c.isMod)
			return;
		if(!%c.isAdmin)
			return;
		if(!isObject(%tar = findClientbyName(%v)))
		{
			messageClient(%c, '', "\c6This client does not exist.");
			return;
		}
		if(!$Server::Muted[%tar.getBLID()])
		{
			messageClient(%c, '', "\c6This client is not muted!");
			return;
		}
		else
		{
			$Server::Muted[%tar.getBLID()] = 0;
			cancel($Server::MuteSch[%tar.getBLID()]);
			messageAllExcept(%tar, -1, 'MsgAdminForce', "\c3" @ %c.getPlayerName() SPC "\c2unmuted\c3" SPC %tar.getPlayerName() @ "\c2.");
			messageClient(%tar, 'MsgAdminForce', "\c3You\c2 have been unmuted by\c3" SPC %c.getPlayerName() @ "\c2.");
		}
	}

	function GameConnection::startLoad(%this)
	{
		if($Server::Muted[%this.getBLID()])
		{
			if($Server::MutedTime[%this.getBLID()])
				%t = $Server::MutedTime[%this.getBLID()];
			$Server::Muted[%this.getBLID()] = 1;
			if(%t != -1)
			{
				cancel($Server::MuteSch[%this.getBLID()]);
				%this.MuteSch(%t | 0);
			}
			scheduleNoQuota(10, 0, messageClient, %this, 'MsgClearBricks', "\c0Uh oh!\c6 You are still muted.. Shame." SPC ($Pref::Server::Mute::MuteRestartonDC && %t != -1 ? "Your mute time has been restarted." : ""));
		}
		Parent::startLoad(%this);
	}

	function serverCMDmessageSent(%client, %text)
	{
		if($Server::Muted[%client.getBLID()])
			messageClient(%client, '', "\c2You cannot talk while muted!");
		else
	 		Parent::serverCMDmessageSent(%client, %text);
	}
	
	function serverCMDteamMessageSent(%client, %text)
	{
		if($Server::Muted[%client.getBLID()])
			messageClient(%client, '', "\c2You cannot talk while muted!");
		else
	 		Parent::serverCMDteamMessageSent(%client, %text);
	}

	function serverCmdStartTalking(%client)
	{
		if($Server::Muted[%client.getBLID()])
 			return;
		return Parent::serverCmdStartTalking(%client);
	}
};activatepackage(mute);
