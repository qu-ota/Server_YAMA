datablock AudioProfile(lightning3ZapSound)
{
	filename = "Add-Ons/Weapon_ElementalSpells/Sounds/lightning3ZapSound.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(lightning3LoopSound)
{
	filename = "Add-Ons/Weapon_ElementalSpells/Sounds/lightning3LoopSound.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock ParticleData(lightning3AmbientParticle)
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
datablock ParticleEmitterData(lightning3AmbientEmitter)
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
	particles = "lightning3AmbientParticle";

	uiName = "lightning3 - Ambient";
};

datablock ParticleData(lightning3TrailParticle)
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
datablock ParticleEmitterData(lightning3TrailEmitter)
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
	particles = "lightning3TrailParticle";

	uiName = "lightning3 - Trail";
};

datablock ParticleData(lightning3ExplosionParticle)
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
datablock ParticleEmitterData(lightning3ExplosionEmitter)
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
	particles = "lightning3TrailParticle";

	uiName = "lightning3 - Explosion";
};

datablock ExplosionData(lightning3Explosion)
{
	soundProfile = lightning3ZapSound;

	lifeTimeMS = 250;

	particleEmitter = lightning3ExplosionEmitter;
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

	uiName = "lightning3 Zap";
};

AddDamageType("lightning3D", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning3> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning3> %1', 1, 1);
AddDamageType("lightning3R", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning3> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_lightning3> %1', 1, 0);

datablock ProjectileData(lightning3Bolt)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage = 15;
	directDamageType = $DamageType::lightning3D;
	radiusDamageType = $DamageType::lightning3R;

	brickExplosionRadius = 4;
	brickExplosionImpact = true;
	brickExplosionForce = 1;
	brickExplosionMaxVolume = 20;
	brickExplosionMaxVolumeFloating = 35;

	impactImpulse = 400;
	verticalImpulse = 400;
	explosion = lightning3Explosion;
	particleEmitter = lightning3TrailEmitter;
	sound = lightning3LoopSound;

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

	uiName = "lightning3 Bolt";
};

function lightning3Bolt::radiusDamage(%this, %obj, %target, %amt, %pos, %dist)
{
	if(%obj.OHKO)
	{
		%target.damage(%obj, %pos, 10000, $DamageType::lightning3R);
		%target.burnPlayer(5100);
	}
	else
	{
		parent::radiusDamage(%this, %obj, %target, %amt, %pos, %dist);
	}
}

datablock ItemData(lightning3Item)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/lightning3.dts";
	rotate = false;
	mass = 0.2;
	density = 0.1;
	elasticity = 0.8;
	friction = 0.2;
	emap = true;

	uiName = "Magic - Lightning 2";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_lightning3";
	doColorShift = false;
	colorShiftColor = "1 1 0 1";

	image = lightning3Image;
	canDrop = false;
};

datablock ShapeBaseImageData(lightning3Image)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = ("0 0 0");

	correctMuzzleVector = true;

	className = "WeaponImage";
	item = lightning3Item;
	ammo = " ";
	MPused = 20;
	projectile = lightning3Bolt;
	projectileType = Projectile;

	melee = false;
	armReady = true;
	
	lightType = "ConstantLight";
	lightColor = "1 1 0";
	lightRadius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = lightning3ZapSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = lightning3AmbientEmitter;
	stateEmitterTime[1]            = 600;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Ready";
	stateTimeoutValue[2]           = 0.25;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;
};

function lightning3Image::onFire(%this, %obj, %slot)
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

package Magiclightning3
{
	function armor::damage(%this, %obj, %sourceObject, %pos, %amt, %type)
	{
		if(%type == $DamageType::lightning3R || %type == $DamageType::lightning3D)
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
		if(%this.isMage && %obj.getMountedImage(0) == lightning3Image.getID() && %val && %slot == 4)
		{
			if(%obj.hasEnergy(100))
			{
				%proj = new projectile()
				{
					datablock = lightning3Bolt;
					initialPosition = vectorAdd(%obj.getPosition(), "0 0 " @ getWord(%obj.getScale(), 2) * 50);
					initialVelocity = "0 0 -150";
					scale = vectorScale(%obj.getScale(), 5);
					sourceObject = %obj;
					sourceSlot = 0;
					client = %obj.client;
					OHKO = true;
				};
				missionCleanup.add(%proj);
				%obj.schedule(100, damage, %obj, %obj.getPosition(), 10000, $DamageType::lightning3D);
			}
			else if(isObject(%client = %obj.getControllingClient()))
			{
				%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/100", 3);
			}
		}
	}
};
activatePackage(Magiclightning3);