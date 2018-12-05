//Script_mute

	$Pref::Server::AdvMute::ModCanMute = true;
	$Pref::Server::AdvMute::MaxTime = 60;

	
package mute
{
	function serverCmdMute(%client,%victim,%time)
	{
		%victim = findClientByName(%victim);
		
		if(%victim.isModerator || %victim.isAdmin)
		{	
			messageClient(%client, '', "\c6You can't mute server staff"); 
			return;
		}
		
		if(%client.isAdmin || ($Pref::Server::AdvMute::ModCanMute & %client.isModerator))
		{
			%time = mFloor(%time);
			
			if(%time < -1 || %time $= 0 || %time $= "")
			{
				messageClient(%client, '', "\c3Error: You must enter a valid amount of time in \c2minutes"); 
				return;
			}
			
			if(%time > $Pref::Server::AdvMute::MaxTime)
				%time = $Pref::Server::AdvMute::MaxTime;
		
			if(isObject(%victim))
			{
				if(%victim.isMuted)
				{
					messageClient(%client,'',"\c6" @ %victim.name SPC "is already muted. Expires in" SPC $AdvMute[%victim.getBLID()] SPC "minute(s)");
					return;
				}
					%victim.AdvMuteTick_INIT = 0;
					%victim.isMuted = 1;
					$AdvMute[%victim.getBLID()] = %time;
					
					if(%time == -1)
					{
						if(%client.bl_id == getNumKeyID())
						{
							StaffAnnouncement("Staff Announcement:" SPC %victim.name SPC "was \c2permanently \c3muted by" SPC %client.name);
						}
						else
						{
							messageClient(%client,'',"\c6Only the host can set permanent mutes.");
						}
					}
					else
					{
						StaffAnnouncement("Staff Announcement:" SPC %victim.name SPC "was muted by" SPC %client.name SPC "for\c2" SPC %time SPC "\c3minutes.");
						cancel($AdvMuteSch[%victim.getblid()].muteSchedule);
						$AdvMuteSch[%victim.getblid()].muteSchedule = %victim.AdvMute_Tick();
					}
					
					AdvMute_AddMessage(%client.getPlayerName() TAB %time TAB %victim.getPlayerName());
			}
			else
				messageClient(%client,'',"\c7Can't find that player.");
		}
	}
	
	function serverCmdUnMute(%client, %victim)
	{
		if(%client.isModerator || %client.isAdmin)
		{
			%victim = findClientByName(%victim);
			if(isObject(%victim))
			{
				if(!%victim.isMuted)
				{
					messageClient(%client, '', "\c7This player is not muted.");
					return;
				}
				
				%victim.isMuted = 0;
				$AdvMute[%victim.getblid()] = 0;
				StaffAnnouncement("Staff Announcement:" SPC %victim.name SPC "was un-muted by" SPC %client.name);
				cancel($AdvMuteSch[%victim.getblid()].muteSchedule);
				AdvMute_AddMessage(%client.getPlayerName() TAB "unmutes" TAB %victim.getPlayerName());
			}
			else
				messageClient(%client,'',"\c7Player not found.");
		}
	}
   
	function serverCmdMessageSent(%client, %text)
	{
		if(%client.isMuted)
		{
			messageClient(%client,'',"\c6Sorry, you're muted.  Expires in" SPC $AdvMute[%client.getBLID()] SPC "minute(s)");
			return;
		}
		parent::serverCmdMessageSent(%client, %text);
	}
	
	function serverCmdTeamMessageSent(%client, %text)
	{
		if(%client.isMuted)
		{
			messageClient(%client,'',"\c6Sorry, you're muted.  Expires in" SPC $AdvMute[%client.getBLID()] SPC "minute(s)");
			return;
		}
		parent::serverCmdTeamMessageSent(%client, %text);
	}
	
	function GameConnection::AutoAdminCheck(%this)
	{
		if($AdvMute[%this.getBLID()] > 0)
		{
			messageClient(%this,'',"\c6Your mute will expire in\c0 " @ $AdvMute[%this.getBLID()] @ " minute(s)\c7.");
			%this.AdvMute_Tick();
		}
		else if($AdvMute[%this.getBLID()] == -1)
		{
			%this.isMuted = 1;
			messageClient(%this,'',"\c6You are permanently muted.");
		}
		return parent::AutoAdminCheck(%this);
	}
	
	function GameConnection::AdvMute_Tick(%this)
	{
		if(!isObject(%this))
			return;
		cancel($AdvMuteSch[%this.getBLID()].muteSchedule);
		if($AdvMute[%this.getBLID()] > 0)
		{
			%this.isMuted = 1;
			if(!%this.AdvMuteTick_INIT)
				%this.AdvMuteTick_INIT = 1;
			else
				$AdvMute[%this.getBLID()]--;
			$AdvMuteSch[%this.getBLID()].muteSchedule = %this.schedule(60000,AdvMute_Tick);
		}
		else
		{
			if(%this.isMuted)
			{
				%this.isMuted = 0;
				messageClient(%this,'',"\c7Your mute has expired.");
			}
		}
	}
	
	function AdvMute_AddMessage(%msg)
	{
		%file = new FileObject();
		if(!isFile("config/server/mute_Logs.txt"))
		{
			%file.openForWrite("config/server/mute_Logs.txt");
			%file.writeLine("Staff Name" TAB "Mute Duration" TAB "Muted Name");
			%file.writeLine(%msg);
		}
		else
		{
			%file.openForAppend("config/server/mute_Logs.txt");
			%file.writeLine(%msg);
		}
		%file.close();
		%file.delete();
	}
	
	function servercmdlistmutes(%client) 
	{
		if (%client.isModerator || %client.isAdmin) 
		{
			MessageClient(%client, '', "\c2Online muted users:");
			for (%clientIndex = 0 ; %clientIndex < ClientGroup.getCount() ; %clientIndex++) 
			{
				%cl = ClientGroup.getObject(%clientIndex);
				if (%cl.isMuted) 
				{
					MessageClient(%client, '',"\c6" @ %cl.name SPC "- expires in\c2 " @ $AdvMute[%client.getBLID()] @ " \c6minute(s)");
				}
			}
			MessageClient(%client, '', "\c2End of list");
		}
	}
	
};
activatePackage(mute);

