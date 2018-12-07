//Weapon_ElementalSpells.cs
//By Amadé (ID 10716)
//
//Thanks to ClickTeam for their very nice sound resources
// *** http://www.clickteam.com/ *** //

datablock PlayerData(PlayerNoMoveArmor : PlayerStandardArmor)
{
	airControl = 0;
	jumpForce = 0;
	runForce = 0;
	maxForwardSpeed = 0;
	maxSideSpeed = 0;
	maxBackwardSpeed = 0;
	maxForwardCrouchSpeed = 0;
	maxSideCrouchSpeed = 0;
	maxBackwardCrouchSpeed = 0;
	jumpSound = "";
	canRide = false;
	canJet = false;
	cameraDefaultFov = 90;
	cameraMaxFov = 90;
	cameraMinFov = 90;
	uiName = "";
};

datablock PlayerData(PlayerMageArmor : PlayerStandardArmor)
{
	rechargeRate = 0.25;
	showEnergyBar = true;
	canJet = false;
	uiName = "Magician";
	isMage = true;
};
datablock PlayerData(PlayerFPmageArmor : PlayerMageArmor)
{
	firstPersonOnly = true;
	uiName = "Magician (FP)";
};

function Player::HasEnergy(%obj, %amt)
{
	if(%obj.getEnergyLevel() >= %amt)
	{
		return true;
	}
	return false;
}

function Player::isOnGround(%obj)
{
	%pos = %obj.getPosition();
	%end = vectorAdd(%pos, "0 0 -0.1");
	%typemasks = $TypeMasks::FxBrickAlwaysObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::VehicleObjectType;
	%ray = containerRayCast(%pos, %end, %typemasks, %obj);
	if(isObject(%hit = firstWord(%ray)))
	{
		if(%hit.getClassName() !$= "FxDTSbrick")
		{
			return true;
		}
		else if(%hit.isColliding())
		{
			return true;
		}
	}
	return false;
}

exec("./Scripts/Lightning.cs");
exec("./Scripts/Fire.cs");
exec("./Scripts/Wind.cs");
exec("./Scripts/Water.cs");
exec("./Scripts/Ice.cs");
exec("./Scripts/Stone.cs");
exec("./Scripts/Darkness.cs");
exec("./Scripts/Light.cs");
exec("./Scripts/Light2.cs");
exec("./Scripts/Lightning2.cs");
exec("./Scripts/Water2.cs");
exec("./Scripts/Fire2.cs");
exec("./Scripts/Lightning3.cs");
exec("./Scripts/Lightning4.cs");
exec("./Scripts/Lightning5.cs");
exec("./Scripts/Water3.cs");
exec("./Scripts/Fire3.cs");
exec("./Scripts/Fire4.cs");
exec("./Scripts/Fire5.cs");

registerOutputEvent("Player", "Freeze", "");

function Player::Freeze(%obj)
{
	IceMProjectile.damage(0, %obj, 0, %obj.getPosition(), "0 0 0");
}

registerOutputEvent("Player", "SetOnFire", "");

function Player::SetOnFire(%obj)
{
	FireMProjectile.RadiusDamage(0, %obj, 0, %obj.getPosition(), 0);
}