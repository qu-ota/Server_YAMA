//YAMA (Yet Another Moderator Addon)
//By Dominoes (37977)
//A better moderator addon, including moderator-supported module loading
//DONT TOUCH THIS UNLESS YOU KNOW WHAT YOU ARE DOING

echo("=== YAMA | Checking version ===");
$YAMA::General::Version = "1.0.0 Release";
if(!$YAMA::General::Version = "1.0.0 Release")
	return error("=== YAMA | Version check failed ===");

echo("=== YAMA | Loading core module ==="); //Ahead Client Tools by Ahead, edited to load everything from YAMA instead of "Client_Ahead"
function loadMainYAMAModules()
{
	if(isFile("Add-Ons/Server_YAMA/core.cs") && isFile("Add-Ons/Server_YAMA/lists.cs") && isFile("Add-Ons/Server_YAMA/commands.cs"))
	{
		echo("=== YAMA | Core files found, loading ===");
		exec("Add-Ons/Server_YAMA/core.cs");
		exec("Add-Ons/Server_YAMA/lists.cs");
		exec("Add-Ons/Server_YAMA/commands.cs");
		$YAMA::coresLoaded = 1;
	}
	else
	{
		$YAMA::coresLoaded = 0;
		return error("=== YAMA | Core file not found, not executing ===");
	}
}

function loadYAMAModules() //function for finding & loading modules
{
	%search = "Add-Ons/Server_YAMA/modules/*.cs";

	for(%file = findFirstFile(%search); isFile(%file); %file = findNextFile(%search))
	{
		echo("=== YAMA | Loading module:" SPC %file);
		exec(%file);
	}
}
echo("=== YAMA | Ensuring core files are present... ===");
loadMainYAMAModules();
echo("=== YAMA | Ensuring everything's good so far... ===");
if($YAMA::coresLoaded)
{
	continue;
}
else
{
	return error("=== YAMA | Add-on not loaded, cancelling ===");
}
echo("=== YAMA | Everything looks good and loaded properly, now looking for modules ===");
loadYAMAModules();
echo("=== YAMA version" SPC $YAMA::General::Version SPC "has loaded successfully! ===");
