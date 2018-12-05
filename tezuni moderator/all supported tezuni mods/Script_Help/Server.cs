//////////////////
//Help Command//////
//////////////////
//Author: Tezuni//
//////////////////

function servercmdHelp(%client,%topic)
{
	if(%topic $= "")
	{
		messageClient(%client, '', "\c6Please Choose a Topic. (Ex: /help rules)");
		messageClient(%client, '', "\c6-->\c0rules");
		messageClient(%client, '', "\c6-->\c1regular");
		messageClient(%client, '', "\c6-->\c4moderator");
		messageClient(%client, '', "\c6-->\c3admin");
		messageClient(%client, '', "\c6-->\c5donations \c0(No Donations Currently)");
		messageClient(%client, '', "\c6-->\c2Admin\c6-\c0rules");
		messageClient(%client, '', "\c6-->\c0Commands");
		return;
	}

	if(%topic $= "rules")
	{
		messageClient(%client, '', "\c0These are the rules! Please read them!!");
		
		messageClient(%client, '', "\c61. Do not waste the time of rounds at all! (Ex: Staying at spawn, Walking of without a kart)");
		messageClient(%client, '', "\c62. Absolutely NO linking Porn or other bad links");
		messageClient(%client, '', "\c6   (No exceptions, warnings, mutes, or kicks! Instant <color:ff0000>Ban<color:ffffff>!!)");
		messageClient(%client, '', "\c63. No asking for Reg, Mod, Admin, or SA.");
		messageClient(%client, '', "\c6   -People have been asking ranks for their birthday. The");
		messageClient(%client, '', "\c6    rule still applies when asking for ranks on a special occasion!");
		messageClient(%client, '', "\c64. Do NOT complain to the staff! (Mod, Admin, Super Admin, Owner.)");		
		messageClient(%client, '', "\c65. No spamming chat. Use caps reasonably!");
		messageClient(%client, '', "\c66. Do NOT make fun of a person, be nice and help them!");
		messageClient(%client, '', "\c67. No ramming just to annoy people");

		messageClient(%client, '', "\c0Any questions about the rules? Ask a Mod, Admin, SA, or the owner.");
		messageClient(%client, '', "\c6 To scroll through the rules, use \c3Page Up \c6and \c3Page Down.");
		servercmdRules(%client);
		return;
	}

	if(%topic $= "regular")
	{
		messageClient(%client, '', "\c1Regulars \c6are chosen for two reasons:");
		messageClient(%client, '', "   \c61. Frequently playing on this server");
		messageClient(%client, '', "   \c62. Playing by and helping others learn the rules");
		return;
	}

	
	if(%topic $= "moderator")
	{
		messageClient(%client, '', "\c4Moderators \c6are chosen for three reasons:");
		messageClient(%client, '', "   \c61. Being a successfull server \c1regular");
		messageClient(%client, '', "   \c62. Knowing the the official rules (/help rules)");
		messageClient(%client, '', "   \c63. Having fair and good judgment in all situations");
		return;
	}
	
	if(%topic $= "admin")
	{
		messageClient(%client, '', "\c3Admins \c6are chosen for two reasons:");
		messageClient(%client, '', "   \c61. Being a successfull server \c4moderator");
		messageClient(%client, '', "   \c62. Being able to contribute something unique to the server (Ex. make an effortful Speedkart map.)");
		return;
	}

	if(%topic $= "donations")
	{
		messageClient(%client, '', "\c6Donations:");
		messageClient(%client, '', "   \c61. Sorry, there is no donation for the server currently. :/");
		return;
	}
		if(%topic $= "admin-rules")
	{
		messageClient(%client, '', "\c2Admin \c6Rules:");
		messageClient(%client, '', "\c61. If you want to build, build on the admin lounge!! Make sure it does not interfere with the speedkart maps!");
		messageClient(%client, '', "\c62. The basic rules still apply to you! If you break one, you may still be demoted!");
		messageClient(%client, '', "\c63. If you want to rank someone, ask me first! I don't want to see \c0over 9000 \c6people with Regular... :/");
		return;
	}
		if(%topic $= "commands")
	{
		messageClient(%client, '', "\c6	Commands:");
		messageClient(%client, '', "\c1Normal \c6and Higher Ranked Players:");

		messageClient(%client, '', "  \c6- \c0/boombox\c6 - Use this command to rock your choice of music from your player!");
		messageClient(%client, '', "  \c6- \c0/me (Message with spaces)\c6 - Use this to express what your character is doing.");
		messageClient(%client, '', "\c4Moderators \c6 and Higher Ranked Players:");
		messageClient(%client, '', "  \c6- \c0/ban [name (can use BLID instead)] [BLID (can use name instead)] [time] [reason]\c6 - Bans a person (Mods have limit of 10 min.)");
		messageClient(%client, '', "  \c6- \c0/kick [Name]\c6 - Kicks a player");
		messageClient(%client, '', "  \c6- \c0/Mute [Username] [Time]\c6 - Mutes a player");
		messageClient(%client, '', "  \c6- \c0/Warn [Username] [Warning text (Spaces are available)]\c6 - Mutes a player");
		messageClient(%client, '', "\c2Admin \c6 and \c0Super Admin:");
		messageClient(%client, '', "\c6- \c0/colortest\c6 - Displays \c00\c11\c22\c33\c44\c55\c66\c77\c88 \c6 to the public in chat.");
		messageClient(%client, '', "\c6- \c0/reg [Username]\c6 - Ranks a player up to Regular (ask owner first!)");
		messageClient(%client, '', "\c6- \c0/reg [Username]\c6 - Demotes a player from Regular, they will no longer have a rank (ask owner first!)");

		messageClient(%client, '', "\c0Super Admin\c6 Ranked Players:");
		messageClient(%client, '', "\c6- \c0/Mod [Username] \c6- Ranks someone from a Reg to a Mod (ask owner first!)");
		messageClient(%client, '', "\c6 - \c0/demod [username] \c6- Demotes a player from Moderator, puts them back to Reg (ask me first!)");
		messageClient(%client, '', "\c6Any questions about the \c0commands\c6? Feel free to ask anyone!");
		messageClient(%client, '', "\c6 To scroll through the \c0commands\c6, use \c3Page Up \c6and \c3Page Down\c6.");



		return;
	}












	
}

