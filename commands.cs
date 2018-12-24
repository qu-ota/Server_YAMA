//Extra commands for extra needy people

function serverCmdYama(%cl,%dis,%cat,%param)
{
	if(%dis $= "help")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c3YAMA \c6(Yet Another Moderator Add-On) \c3Help Menu");
		messageClient(%cl,'',"\c6== Specify your query by using \c3/yama category ==");
		messageClient(%cl,'',"\c2about \c7-- \c6Everything you need to know about this add-on.");
		messageClient(%cl,'',"\c3features \c7-- \c6A list of things of all the stuff included in this add-on.");
		messageClient(%cl,'',"\c4commands \c7-- \c6All the general commands included in this add-on.");
		messageClient(%cl,'',"\c5modules \c7-- \c6What modules are, and which ones are in the server.");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "about")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c3YAMA \c6(Yet Another Moderator Add-On) by \c3Dominoes (37977)");
		messageClient(%cl,'',"\c6Yet Another Moderator Addon is a moderator add-on seen in the eyes of Dominoes, with introductions of lots of new features.");
		messageClient(%cl,'',"\c6This includes updating the player list with one's status. moderator orbing and fetch/finding, and even module loading.");
		messageClient(%cl,'',"\c6To find out more about what this add-on has, use \c3/yama features \c6in chat.");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "features")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c6Features of \c3YAMA \c6(Yet Another Moderator Add-On)");
		messageClient(%cl,'',"\c7==== \c3Module Loading \c7====");
		messageClient(%cl,'',"\c6No more having to hassle with adding lines of code to load new modules or using separate add-ons with moderator-supported scripts.");
		messageClient(%cl,'',"\c6With YAMA, you can simply drag all your custom moderator-supported scripts into the included \c3modules \c6folder, and simply reload your server.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3Playerlist Labeling \c7====");
		messageClient(%cl,'',"\c6Confused with who's a moderator? Worry no more, as Blockland Glass's 'Glass_setPlayerListStatus' function can update the playerlist with any letter one wants.");
		messageClient(%cl,'',"\c6Admins and other users can tell who's a moderator and who isn't.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3Admin-only tools, expanded for moderator usage \c7====");
		messageClient(%cl,'',"\c6Moderators can now use the admin orb, alongside /warp, /fetch, and /find.");
		messageClient(%cl,'',"\c6Everyone gets notified when admins and moderators use either /fetch or /find, so people can determine who's cheating in games and who isn't.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3Auto-or-Manual Moderator \c7====");
		messageClient(%cl,'',"\c6Don't want someone to become a moderator when they rejoin? Don't fret, as one can make someone a manual moderator, and loses the status when they rejoin.");
		messageClient(%cl,'',"\c6Or, simply put \c3a \c6or \c3auto \c6after the /mod command and you'll make them an automatic moderator.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c6Get information on the modules on the server with \c3/yama modules \c6. \c7====");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "modules" && %cat $= "")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c6Modules menu \c7-- \c3YAMA");
		messageClient(%cl,'',"\c7==== \c6Use \c3/yama modules category \c6to read more about said category. \c7====");
		messageClient(%cl,'',"\c2info \c7-- \c6What 'modules' are in this add-on.");
		messageClient(%cl,'',"\c4active \c7-- \c6All active modules on the server.");
		messageClient(%cl,'',"\c5creation \c7-- \c6Learn how to create modules and have them load into your server.");
		messageClient(%cl,'',"\c3minfo <module> \c7-- \c6Get information about a module.");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "modules" && %cat $= "info")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c6Module menu \c7-- \c6Information \c7-- \c3YAMA");
		messageClient(%cl,'',"\c6Modules are scripts put into the folder inside this add-on. All modules get loaded when this add-on loads.");
		messageClient(%cl,'',"\c6The folder is composed of all this add-on's included modules, and any that you add in yourself.");
		messageClient(%cl,'',"\c0Make sure your modules support Moderator add-ons and has a function that shows all the commands! \c6Add checks in your script that check if one is or is not a moderator with \c3%var.isModerator\c6.");
		messageClient(%cl,'',"\c6To see all the active modules in this server, say \c3/yama modules active\c6.");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "modules" && %cat $= "active")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c6Module menu \c7-- \c6Active Modules \c7-- \c3YAMA");
		messageClient(%cl,'',"\c2Here are the active modules in this server right now:");
		messageClient(%cl,'',"\c7======");
		%search = "Add-Ons/Server_YAMA/modules/*.cs";
		for(%file = findFirstFile(%search); isFile(%file); %file = findNextFile(%search))
		{
			%file = strReplace(%file,"Add-Ons/Server_YAMA/modules/","");
			messageClient(%cl,'',"\c3" @ %file);
		}
		messageClient(%cl,'',"\c6Say \c3/yama modules minfo <modulename> \c6to see information on a module, if one is found. \c0Do not include \c6.cs \c0or \c6<> \c0in the module names!");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "modules" && %cat $= "creation")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3YAMA Module Creation Guide \c7====");
		messageClient(%cl,'',"\c6So you want to make your own modules and have them loaded with this add-on. That's cool!");
		messageClient(%cl,'',"\c0However\c6, you'll need a few things in your script before you think about letting it load:");
		messageClient(%cl,'',"\c7== \c3Moderator Checks \c7==");
		messageClient(%cl,'',"\c6Make sure the add-on actually has support for moderators.");
		messageClient(%cl,'',"\c7That means adding checks such as \c3if(%client.isModerator) \c7in your script. Without this it sorta becomes useless and unneeded.");
		messageClient(%cl,'',"\c7== \c3Help Variables \c7==");
		messageClient(%cl,'',"\c6Ensure you have variables stated in your script that shows information about it.");
		messageClient(%cl,'',"\c3YAMA will say that there's no information found if your script doesn't explicitly state anything about it.");
		messageClient(%cl,'',"\c7Look in other modules to see how it works. The variables should be \c6$YAMA::ModuleName::Name\c7, \c6$YAMA::ModuleName::Author\c7, \c6$YAMA::ModuleName::Commands\c7, and \c6$YAMA::ModuleName::Information\c7.");
		messageClient(%cl,'',"\c7Add the information into the script at the top or bottom. Again, look inside other functions to see how it works.");
		messageClient(%cl,'',"\c7== \c3Make sure it works \c7==");
		messageClient(%cl,'',"\c6Nothing's worse than a broken script. Ensure that it works before letting others use it. Not much else to say.");
		messageClient(%cl,'',"\c3Once you've done/added the prerequisites, drag your script into the modules folder in the add-on. You'll be all set!");
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "modules" && %cat $= "minfo")
	{
		if(!isFile("Add-Ons/Server_YAMA/modules/" @ %param @ ".cs"))
			return messageClient(%cl,'',"\c6Could not find module \c3" @ %param @ "\c6. Say \c3/yama modules active \c6to see all active modules.");
		if(%param $= "")
			return messageClient(%cl,'',"\c6You need to put in a module name.");
		
		if($YAMA["::" @ %param @ "::Name"] !$= "")
			%name = $YAMA["::" @ %param @ "::Name"];
		else
			%name = "[NOT SET]";
		
		if($YAMA["::" @ %param @ "::Author"] !$= "")
			%author = $YAMA["::" @ %param @ "::Author"];
		else
			%author = "[NOT SET]";
		
		if($YAMA["::" @ %param @ "::Commands"] !$= "")
			%commands = $YAMA["::" @ %param @ "::Commands"];
		else
			%commands = "[NOT SET]";
		
		if($YAMA["::" @ %param @ "::Information"] !$= "")
			%info = $YAMA["::" @ %param @ "::Information"];
		else
			%info = "[NOT SET]";
		
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3YAMA Module Information: \c6" @ %param @ ".cs \c7====");
		messageClient(%cl,'',"\c6Name: \c3" @ %name);
		messageClient(%cl,'',"\c6Author: \c3" @ %author);
		messageClient(%cl,'',"\c6Commands: \c3" @ %commands);
		messageClient(%cl,'',"\c6Descrption: \c3" @ %info);
		messageClient(%cl,''," ");
		return;
	}
	if(%dis $= "commands")
	{
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c7==== \c3YAMA Commands List\c7====");
		messageClient(%cl,'',"\c6Here's a list of all the addon's included commands:");
		messageClient(%cl,'',"\c3/mod victim type");
		messageClient(%cl,'',"\c7Rank required: Super Admin/Admin, depends on pref set");
		messageClient(%cl,'',"\c6Makes someone a moderator. Change \c3type \c6to \c3a \c6or \c3auto \c6to make them an auto moderator.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c3/demod victim");
		messageClient(%cl,'',"\c7Rank required: Super Admin/Admin, depends on pref set");
		messageClient(%cl,'',"\c6Removes moderator status of someone. Will always remove auto status.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c3/listModerators \c6(alias: /listMods)");
		messageClient(%cl,'',"\c7Rank required: Moderator");
		messageClient(%cl,'',"\c6Lists all active moderators on the server.");
		messageClient(%cl,''," ");
		messageClient(%cl,'',"\c3/yama help");
		messageClient(%cl,'',"\c7Rank required: None");
		messageClient(%cl,'',"\c6Shows the help menu. Follow the instructions to find what you need.");
		messageClient(%cl,''," ");
		return;
	}
	else
		messageClient(%cl,'',"\c6Say \c3/yama help \c6to see the help menu for this addon.");
}
