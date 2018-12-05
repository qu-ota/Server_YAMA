package Script_ModShields
{
	function serverCmdMessageSent(%client,%msg)
	{
		%oldPrefix = %client.clanPrefix;
		if(%client.isModerator)
		{
			%client.clanPrefix = "<bitmap:add-ons/script_modshield/icon_cyanBadge.png>\c7 " @ %oldPrefix;
		}
		Parent::serverCmdMessageSent(%client,%msg);
		%client.clanPrefix = %oldPrefix;
	}
};
activatepackage(Script_ModShield);
datablock DecalData(ModShield)
{
	textureName = "add-ons/script_modshield/icon_cyanBadge.png";
};