//YAMA (Yet Another Moderator Addon)
//By Dominoes (37977)
//A better moderator addon, including moderator-supported module loading and playerlist updating

echo("YAMA | Loading main functions"); //Ahead Client Tools by Ahead, edited to load everything from YAMA instead of "Client_Ahead"
function loadMainYAMAModules()
{
	if(isFile("Add-Ons/Server_YAMA/core.cs"))
	{
		echo("YAMA | Core found, loading and continuing");
		exec("Add-Ons/Server_YAMA/core.cs");
		$YAMA::coresLoaded = 1;
	}
	else
	{
		$YAMA::coresLoaded = 0;
		return error("YAMA | Core file not found, not executing");
	}
}

function loadYAMAModules() //function for finding & loading modules
{
	%dir = "Add-Ons/Server_YAMA/modules/*.cs";
	%fileCount = getFileCount(%dir);
	%filename = findFirstFile(%dir);
	%dirCount = 0;
	while(%filename !$= "")
	{
		%path = filePath(%filename);
		%dirName = getSubStr(%path, strlen("Add-Ons/Server_YAMA/modules/"), strlen(%path) - strlen("Add-Ons/Server_YAMA/modules/"));
		%dirNameList[%dirCount] = %dirName;
		%dirCount = %dirCount + 1.0;
		%filename = findNextFile(%dir);
	}
	%i = 0;
	while(%i < %dirCount)
	{
		%dirName = %dirNameList[%i];
		echo("YAMA | Checking module: " @ %dirName);
		%name = %dirName;
		echo("YAMA | Loading module: " @ %dirName);
		exec("Add-Ons/Server_YAMA/modules/" @ %dirName);
		%i = %i + 1.0;
	}
}
echo("YAMA | Ensuring core files are present...");
loadMainYAMAModules();
echo("YAMA | Ensuring everything's good so far...");
if($YAMA::coresLoaded)
{
	continue;
}
else
{
	return error("YAMA | Add-on not loaded, cancelling");
}
echo("YAMA | Everything looks good and loaded properly, now looking for modules");
loadYAMAModules();
echo("YAMA | Good to go!");
