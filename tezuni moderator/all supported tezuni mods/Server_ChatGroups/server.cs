ORBS_registerPref("Color code 1", "Staff Chat", "$GC::Color1", "string 100", "Server_ChatGroups", "<color:FF00FF>", 0, 0, "");
ORBS_registerPref("Color code 2", "Staff Chat", "$GC::Color2", "string 100", "Server_ChatGroups", "<color:00FF00>", 0, 0, "");
ORBS_registerPref("Color code 3", "Staff Chat", "$GC::Color3", "string 100", "Server_ChatGroups", "<color:00FF00>", 0, 0, "");

ORBS_registerPref("Color code 1", "Super Admin Chat", "$SAC::Color1", "string 100", "Server_ChatGroups", "<color:6600FF>", 0, 0, "");
ORBS_registerPref("Color code 2", "Super Admin Chat", "$SAC::Color2", "string 100", "Server_ChatGroups", "<color:FFFFFF>", 0, 0, "");
ORBS_registerPref("Color code 3", "Super Admin Chat", "$SAC::Color3", "string 100", "Server_ChatGroups", "<color:FFFFFF>", 0, 0, "");

ORBS_registerPref("Color code 1", "Admin Chat", "$AC::Color1", "string 100", "Server_ChatGroups", "<color:FFFFFF>", 0, 0, "");
ORBS_registerPref("Color code 2", "Admin Chat", "$AC::Color2", "string 100", "Server_ChatGroups", "<color:FF00FF>", 0, 0, "");
ORBS_registerPref("Color code 3", "Admin Chat", "$AC::Color3", "string 100", "Server_ChatGroups", "<color:FFFFFF>", 0, 0, "");

ORBS_registerPref("Color code 1", "moderator Chat", "$MC::Color1", "string 100", "Server_ChatGroups", "<color:FF00FF>", 0, 0, "");
ORBS_registerPref("Color code 2", "moderator Chat", "$MC::Color2", "string 100", "Server_ChatGroups", "<color:00FFFF>", 0, 0, "");
ORBS_registerPref("Color code 3", "moderator Chat", "$MC::Color3", "string 100", "Server_ChatGroups", "<color:00FFFF>", 0, 0, "");

function servercmdgc(%client, %Chat, %Chat2, %Chat3, %Chat4, %Chat5, %Chat6, %Chat7, %Chat8, %Chat9, %Chat10, %Chat11, %Chat12, %Chat13, %Chat14, %Chat15, %Chat16)
{
	if(%client.ismoderator || %client.isadmin || %client.issuperadmin)
	{
		%count = ClientGroup.getCount();
		for(%cl = 0; %cl < %count; %cl++)
		{
			%clientB = ClientGroup.getObject(%cl);
			if(%clientB.ismoderator || %clientB.isAdmin || %clientB.isSuperAdmin)
			{
				Messageclient(%clientb, '', $GC::Color1 @ "GlobalChat(" @ $GC::Color2 @ %client.name @ $GC::Color1 @ ")" @ $GC::Color3 @": "@ %chat SPC %chat2 SPC %chat3 SPC %chat4 SPC %chat5 SPC %chat6 SPC %chat7 SPC %chat8 SPC %chat9 SPC %chat10 SPC %chat11 SPC %chat12 SPC %chat13 SPC %chat14 SPC %chat15 SPC %chat16);
			}
		}
	}
	else
	{
		messageclient(%client, '', "\c3You aren't a moderator!");
	}
}

function servercmdpc(%client, %text)
{
	if(strlen(%text) >= $Pref::Server::MaxChatLen)
		%text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	  
	if(%client.isChattingGlobal || %client.isChattingAdmin || %client.isChattingSuper)
		chatMessageAll(%client, '\c3%1: \c6%2', %client.name, %text);
}

function servercmdtgc(%client)
{
	if(%client.ismoderator || %client.isadmin || %client.issuperadmin)
	{
		if(!%client.isChattingGlobal)
		{
			%client.isChattingGlobal = true;
			messageclient(%client, '', "\c5Global Chat Toggle: \c2ON");
		}
		else
		{
			%client.isChattingGlobal = false;
			messageclient(%client, '', "\c5Global Chat Toggle: \c0OFF");
		}
	if(%client.isChattingAdmin)
		%client.isChattingAdmin=false;
	if(%client.isChattingSuper)
		%client.isChattingSuper=false;
	}
	else
		messageclient(%client, '', "\c2You aren't a staff member!");
}

function servercmdsc(%client, %msg1, %msg2, %msg3, %msg4, %msg5, %msg6, %msg7, %msg8, %msg9, %msg10, %msg11, %msg12, %msg13, %msg14, %msg15, %msg16, %msg17, %msg18)
{
	if(%client.issuperadmin)
	{
		%count = ClientGroup.getCount();
		for(%cl = 0; %cl < %count; %cl++)
		{
			%clientB = ClientGroup.getObject(%cl);
			if(%clientB.issuperadmin)
			{
				Messageclient(%clientb, '', $SAC::Color1 @ "SAdminmsg(" @ $SAC::Color2 @ %client.name @ $SAC::Color1 @ ")" @ $SAC::Color3 @": "@ %msg1 SPC %msg2 SPC %msg3 SPC %msg4 SPC %msg5 SPC %msg6 SPC %msg7 SPC %msg8 SPC %msg9 SPC %msg10 SPC %msg11 SPC %msg12 SPC %msg13 SPC %msg14 SPC %msg15 SPC %msg16 SPC %msg17 SPC %msg18);
			}
		}
	}
	else
	{
		messageclient(%client, '', "\c3You aren't a super admin!");
	}
}

function servercmdtsc(%client)
{
	if(%client.isSuperAdmin)
	{
		if(!%client.isChattingSuper)
		{
			%client.isChattingSuper = true;
			messageclient(%client, '', "\c5SuperAdmin Chat Toggle: \c2ON");
		}
		else
		{
			%client.isChattingSuper = false;
			messageclient(%client, '', "\c5SuperAdmin Chat Toggle: \c0OFF");
		}
	if(%client.isChattingGlobal)
		%client.isChattingGlobal=false;
	if(%client.isChattingAdmin)
		%client.isChattingAdmin=false;
	}
	else
		messageclient(%client, '', "\c0You aren't a super admin!");
}

function servercmdac(%client, %Chat, %Chat2, %Chat3, %Chat4, %Chat5, %Chat6, %Chat7, %Chat8, %Chat9, %Chat10, %Chat11, %Chat12, %Chat13, %Chat14, %Chat15, %Chat16)
{
	if(%client.isadmin)
	{
		%count = ClientGroup.getCount();
		for(%cl = 0; %cl < %count; %cl++)
		{
			%clientB = ClientGroup.getObject(%cl);
			if(%clientB.isadmin)
			{
				Messageclient(%clientb, '', $AC::Color1 @ "AdminChat(" @ $AC::Color2 @ %client.name @ $AC::Color1 @ ")" @ $AC::Color3 @": "@ %chat SPC %chat2 SPC %chat3 SPC %chat4 SPC %chat5 SPC %chat6 SPC %chat7 SPC %chat8 SPC %chat9 SPC %chat10 SPC %chat11 SPC %chat12 SPC %chat13 SPC %chat14 SPC %chat15 SPC %chat16);
			}
		}
	}
	else
	{
		messageclient(%client, '', "\c3You aren't an admin!");
	}
}

function servercmdtac(%client)
{
	if(%client.isAdmin)
	{
		if(!%client.isChattingAdmin)
		{
			%client.isChattingAdmin = true;
			messageclient(%client, '', "\c5Admin Chat Toggle: \c2ON");
		}
		else
		{
			%client.isChattingAdmin = false;
			messageclient(%client, '', "\c5Admin Chat Toggle: \c0OFF");
		}
	if(%client.isChattingGlobal)
		%client.isChattingGlobal=false;
	if(%client.isChattingSuper)
		%client.isChattingSuper=false;
	}
	else
		messageclient(%client, '', "\c5You aren't an admin!");
}

if(isPackage(chatGroups))
{
	deactivatePackage(chatGroups);
}

package chatGroups
{
	function serverCmdMessageSent(%client, %msg)
	{
	
		%newMsg = strupr(getWord(%msg,0));
		if(%newMsg$="!sc" || %newMsg$="sc" || %newMsg$="@sc" || %newMsg$="#sc" || %newMsg$="$sc")
		{
//			echo("SC!!!");
			if(%client.issuperadmin)
			{
				echo("A:" @ %msg);
				%msg = stripMLControlChars(trim(%msg));
				%length = strlen(%msg);
				echo("L:" @ %length);

				%msg = getSubStr(%msg,3,%length);
				echo("B:" @ %msg);
				//URLs
				for(%i=0; %i < getWordCount(%msg); %i++)
				{
					%word = getWord(%msg,%i);
					%url  = getSubStr(%word,7,strLen(%word));

					if(getSubStr(%word,0,7) $= "http://" && strPos(%url,":") == -1)
					{
						%word = "<sPush><a:" @ %url @ ">" @ %url @ "</a><sPop>";
						%msg = setWord(%msg,%i,%word);
					}
				}

				//URLs
				for(%i=0; %i < getWordCount(%msg); %i++)
				{
					%word = getWord(%msg,%i);
					%url  = getSubStr(%word,8,strLen(%word));

					if(getSubStr(%word,0,8) $= "https://" && strPos(%url,":") == -1)
					{
						%word = "<sPush><a:" @ %url @ ">" @ %url @ "</a><sPop>";
						%msg = setWord(%msg,%i,%word);
					}
				}


				
//				%count = ClientGroup.getCount();
				for(%cl = 0; %cl < ClientGroup.getCount(); %cl++)
				{
					%clientB = ClientGroup.getObject(%cl);
					if(%clientB.issuperadmin)
					{
						Messageclient(%clientb, '', $SAC::Color1 @ "SAdminmsg(" @ $SAC::Color2 @ %client.name @ $SAC::Color1 @ ")" @ $SAC::Color3 @": "@ %msg);
					}
				}
			}
			else
			{
				messageclient(%client, '', "\c3You aren't a super admin!");
			}
		}
		else
		{
			//Super Admin Toggle
			if(%client.isChattingSuper)
			{
				%count = ClientGroup.getCount();
				for(%cl = 0; %cl < %count; %cl++)
				{
					%clientB = ClientGroup.getObject(%cl);
					if(%clientB.issuperadmin)
						Messageclient(%clientb, '', $SAC::Color1 @ "SAdminmsg(" @ $SAC::Color2 @ %client.name @ $SAC::Color1 @ ")" @ $SAC::Color3 @": "@ %msg);
				}
				return;
			}
			//Admin Toggle
			if(%client.isChattingAdmin)
			{
				%count = ClientGroup.getCount();
				for(%cl = 0; %cl < %count; %cl++)
				{
					%clientB = ClientGroup.getObject(%cl);
					if(%clientB.isadmin)
						Messageclient(%clientb, '', $AC::Color1 @ "AdminChat(" @ $AC::Color2 @ %client.name @ $AC::Color1 @ ")" @ $AC::Color3 @": "@ %msg);
				}
				return;
			}
			//Global Toggle
			if(%client.isChattingGlobal)
			{
				%count = ClientGroup.getCount();
				for(%cl = 0; %cl < %count; %cl++)
				{
					%clientB = ClientGroup.getObject(%cl);
					if(%clientB.ismoderator || %clientB.isAdmin || %clientB.isSuperAdmin)
						Messageclient(%clientb, '', $GC::Color1 @ "GlobalChat(" @ $GC::Color2 @ %client.name @ $GC::Color1 @ ")" @ $GC::Color3 @": "@ %msg);
				}
				return;
			}
			return Parent::serverCmdMessageSent(%client, %msg);
		}
	}
};
activatePackage(chatGroups);
