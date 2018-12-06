//Moderator Shields
//Created by Vine and Zack0Wack0
//Slightly changed to make filepaths 

package ModShields
{
	function serverCmdMessageSent(%client,%msg)
	{
		if($YAMA::ShowModShields == 1)
		{
			%oldPrefix = %client.clanPrefix;
			if(%client.isModerator || %client.isMod)
			{
				%client.clanPrefix = "<bitmap:add-ons/server_yama/modules/moderatorBadge.png>\c7 " @ %oldPrefix;
			}
			Parent::serverCmdMessageSent(%client,%msg);
			%client.clanPrefix = %oldPrefix;
		}
	}
};
activatepackage(ModShields);
datablock DecalData(ModShield)
{
	textureName = "add-ons/server_yama/modules/moderatorBadge.png";
}
