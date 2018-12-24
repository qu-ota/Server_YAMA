datablock AudioProfile(lightning5ZapSound)
{
	filename = "Add-Ons/Weapon_ElementalSpells/Sounds/lightning5ZapSound.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(lightning5LoopSound)
{
	filename = "Add-Ons/Weapon_ElementalSpells/Sounds/lightning5LoopSound.wav";
	description = AudioCloseLooping5d;
	preload = true;
};

datablock ParticleData(lightning5AmbientParticle)
{
	dragCoefficient = 3;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.75;
	constantAcceleration = 0;
	lifetimeMS = 250;
	lifetimeVarianceMS = 50;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 0;
	spinRandomMin = -300;
	spinRandomMax = 300;

	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 0.25;
	sizes[1] = 1.1;
};
datablock ParticleEmitterData(lightning5AmbientEmitter)
{
	ejectionPeriodMS = 30;
	periodVarianceMS = 0;
	ejectionVelocity = 1.0;
	velocityVariance = 1.0;
	ejectionOffset = 0.0;
	thetaMin = 0;
	thetaMax = 90;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "lightning5AmbientParticle";

	uiName = "lightning5 - Ambient";
};

datablock ParticleData(lightning5TrailParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.15;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 500;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 10;
	spinRandomMin = -500;
	spinRandomMax = 500;
	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 1;
	sizes[1] = 0;
};
datablock ParticleEmitterData(lightning5TrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1.5;
	ejectionOffset = 0.2;
	thetaMin = 115;
	thetaMax = 175;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "lightning5TrailParticle";

	uiName = "lightning5 - Trail";
};

datablock ParticleData(lightning5ExplosionParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.15;
	constantAcceleration = 0;
	lifetimeMS = 2500;
	lifetimeVarianceMS = 1000;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 10;
	sizes[1] = 15;
};
datablock ParticleEmitterData(lightning5ExplosionEmitter)
{
	ejectionPeriodMS = 150;
	periodVarianceMS = 50;
	ejectionVelocity = 2;
	velocityVariance = 1.5;
	ejectionOffset = 0.2;
	thetaMin = 115;
	thetaMax = 175;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "lightning5TrailParticle";

	uiName = "lightning5 - Explosion";
};

datablock ExplosionData(lightning5Explosion)
{
	soundProfile = lightning5ZapSound;

	lifeTimeMS = 250;

	particleEmitter = lightning5ExplosionEmitter;
	particleDensity = 50;
	particleRadius = 0.5;

	faceViewer = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "30 30 30";
	camShakeAmp = "7 2 7";
	camShakeDuration = 0.6;
	camShakeRadius = 2.5;

	lightStartRadius = 10;
	lightEndRadius = 0;
	lightStartColor = "1 1 0";
	lightEndColor = "1 1 0";

	damageRadius = 5;
	radiusDamage = 2;

	uiName = "lightning5 Zap";
};

AddDamageType("lightning5D", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning5> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning5> %1', 1, 1);
AddDamageType("lightning5R", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning5> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning5> %1', 1, 0);

datablock ProjectileData(lightning5Bolt)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage = 25;
	directDamageType = $DamageType::lightning5D;
	radiusDamageType = $DamageType::lightning5R;

	brickExplosionRadius = 4;
	brickExplosionImpact = true;
	brickExplosionForce = 1;
	brickExplosionMaxVolume = 20;
	brickExplosionMaxVolumeFloating = 35;

	impactImpulse = 400;
	verticalImpulse = 400;
	explosion = lightning5Explosion;
	particleEmitter = lightning5TrailEmitter;
	sound = lightning5LoopSound;

	muzzleVelocity = 55;
	velInheritFactor = 0;

	armingDelay = 0;
	lifetime = 4000;
	fadeDelay = 4000;
	bounceElasticity = 0.5;
	bounceFriction = 0.5;
	isBallistic = false;

	hasLight = false;
	lightRadius = 5;
	lightColor = "1 1 0";

	uiName = "lightning5 Bolt";
};

function lightning5Bolt::radiusDamage(%this, %obj, %target, %amt, %pos, %dist)
{
	if(%obj.OHKO)
	{
		%target.damage(%obj, %pos, 10000, $DamageType::lightning5R);
		%target.burnPlayer(5100);
	}
	else
	{
		parent::radiusDamage(%this, %obj, %target, %amt, %pos, %dist);
	}
}

datablock ItemData(lightning5Item)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/lightning5.dts";
	rotate = false;
	mass = 0.2;
	density = 0.1;
	elasticity = 0.8;
	friction = 0.2;
	emap = true;

	uiName = "Magic - Lightning 4";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_lightning5";
	doColorShift = false;
	colorShiftColor = "1 1 0 1";

	image = lightning5Image;
	canDrop = false;
};

datablock ShapeBaseImageData(lightning5Image)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = ("0 0 0");

	correctMuzzleVector = true;

	className = "WeaponImage";
	item = lightning5Item;
	ammo = " ";
	MPused = 20;
	projectile = lightning5Bolt;
	projectileType = Projectile;

	melee = false;
	armReady = true;
	
	lightType = "ConstantLight";
	lightColor = "1 1 0";
	lightRadius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = lightning5ZapSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = lightning5AmbientEmitter;
	stateEmitterTime[1]            = 600;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Ready";
	stateTimeoutValue[2]           = 0.25;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;
};

function lightning5Image::onFire(%this, %obj, %slot)
{
	if(%obj.lst[%this] > getSimTime())
	{
		return;
	}
	%obj.lst[%this] = getSimTime() + 1000;
	if(%obj.getDatablock().isMage)
	{
		if(%obj.hasEnergy(%this.MPused))
		{
			parent::onFire(%this, %obj, %slot);
			%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.MPused);
		}
		else if(isObject(%client = %obj.getControllingClient()))
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/" @ %this.MPused, 3);
		}
	}
	else
	{
		parent::onFire(%this, %obj, %slot);
	}
}

package Magiclightning5
{
	function armor::damage(%this, %obj, %sourceObject, %pos, %amt, %type)
	{
		if(%type == $DamageType::lightning5R || %type == $DamageType::lightning5D)
		{
			if(%obj.getWaterCoverage() > 0)
			{
				%amt*= 1.5;
			}
			if(%obj.getWaterCoverage() == 1)
			{
				%amt*= 1.5;
			}
		}
		parent::damage(%this, %obj, %sourceObject, %pos, %amt, %type);
	}
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%this.isMage && %obj.getMountedImage(0) == lightning5Image.getID() && %val && %slot == 4)
		{
			if(%obj.hasEnergy(100))
			{
				%proj = new projectile()
				{
					datablock = lightning5Bolt;
					initialPosition = vectorAdd(%obj.getPosition(), "0 0 " @ getWord(%obj.getScale(), 2) * 50);
					initialVelocity = "0 0 -150";
					scale = vectorScale(%obj.getScale(), 5);
					sourceObject = %obj;
					sourceSlot = 0;
					client = %obj.client;
					OHKO = true;
				};
				missionCleanup.add(%proj);
				%obj.schedule(100, damage, %obj, %obj.getPosition(), 10000, $DamageType::lightning5D);
			}
			else if(isObject(%client = %obj.getControllingClient()))
			{
				%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/100", 3);
			}
		}
	}
};
activatePackage(Magiclightning5);