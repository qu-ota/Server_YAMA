datablock AudioProfile(light2ExplosionSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/light2Explosion.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock ParticleData(light2AmbientParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 400;
	lifetimeVarianceMS = 100;
	textureName = "base/data/particles/dot";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;

	colors[0] = "1 0.85 0 1";
	colors[1] = "1 0.95 0 0.8";
	colors[2] = "1 1 1 0";
	sizes[0] = 0.2;
	sizes[1] = 0.2;
	sizes[2] = 0.2;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(light2AmbientEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 1;
	velocityVariance = 0.5;
	ejectionOffset = 0.3;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 720;
	phiVariance = 1;
	overrideAdvance = false;
	particles = light2AmbientParticle;
	uiName = "light2 - Ambient";
};

datablock ParticleData(light2TrailParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 750;
	lifetimeVarianceMS = 100;
	textureName = "base/data/particles/dot";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;

	colors[0] = "1 1 0 0.8";
	colors[1] = "1 1 0 0.5";
	colors[2] = "1 1 1 0";
	sizes[0] = 3;
	sizes[1] = 2.5;
	sizes[2] = 2;
	times[0] = 0;
	times[1] = 0.5;
	times[2] = 1;
};
datablock ParticleEmitterData(light2TrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset = 0;
	thetaMin = 0;
	thetaMax = 0;
	phiReferenceVel = 0;
	phiVariance = 0;
	overrideAdvance = false;
	particles = light2TrailParticle;
	uiName = "light2 - Trail";
};

datablock ParticleData(light2ExplosionParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.2;
	gravityCoefficient = -0.1;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 5000;
	lifetimeVarianceMS = 3000;
	textureName = "base/data/particles/cloud";
	spinSpeed = 0;
	spinRandomMin = -100;
	spinRandomMax = 100;
	useInvAlpha = true;

	colors[0] = "1 1 0.5 0";
	colors[1] = "1 1 0.9 0.5";
	colors[2] = "1 1 1 0.3";
	colors[3] = "1 1 1 0";
	sizes[0] = 5;
	sizes[1] = 6;
	sizes[2] = 7;
	sizes[3] = 8;
	times[0] = 0;
	times[1] = 0.1;
	times[2] = 0.7;
	times[3] = 1;
};
datablock ParticleEmitterData(light2ExplosionEmitter)
{
	ejectionPeriodMS = 1000;
	periodVarianceMS = 500;
	ejectionVelocity = 6;
	velocityVariance = 5;
	ejectionOffset = 1;
	thetaMin = 0;
	thetaMax = 90;
	phiReferenceVel = 0;
	phiVariance = 0;
	overrideAdvance = false;
	particles = light2ExplosionParticle;
};

datablock ExplosionData(light2MExplosion)
{
	lifeTimeMS = 1000;

	particleEmitter = light2ExplosionEmitter;
	particleDensity = 75;
	particleRadius = 3;
	soundProfile = light2ExplosionSound;
	
	faceViewer = true;
	explosionScale = "1 1 1";

	emitter[0] = light2ExplosionEmitter;
	
	shakeCamera = false;
	camShakeFreq = "1 1 1";
	camShakeAmp = "1 1 1";
	camShakeDuration = 0;
	camShakeRadius = 0;

	light2StartRadius = 20;
	light2EndRadius = 0;
	light2StartColor = "1 1 1";
	light2EndColor = "1 1 1";

	damageRadius = 11;
	radiusDamage = 1;

	impulseRadius = 11;
	impulseForce = 4500;
};

AddDamageType("light2M", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_light2> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_light2> %1', 0.5, 0);

datablock ProjectileData(light2MProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
	directDamage = 0;
	directDamageType = $DamageType::light2M;
	radiusDamageType = $DamageType::light2M;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;
   brickExplosionForce = 0;
   brickExplosionMaxVolume = 1;
   brickExplosionMaxVolumeFloating = 2;

   impactImpulse = 0;
   verticalImpulse = 0;
   particleEmitter = light2TrailEmitter;
   explosion = light2MExplosion;

   muzzleVelocity = 90;
   velInheritFactor = 1;

   armingDelay = 0;
   lifetime = 4000;
   fadeDelay = 3500;
   bounceElasticity = 0.5;
   bounceFriction = 0.2;
   isBallistic = true;
   gravityMod = 0;

   haslight2 = false;
   light2Radius = 10;
   light2Color = "1 1 1";

   uiName = "light2 Ray";
};
datablock ItemData(light2MItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/light2.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - light2";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_light2";
	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	image = light2MImage;
	canDrop = true;
};

datablock ShapeBaseImageData(light2MImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = light2MItem;
   ammo = " ";
	MPused = 40;
   projectile = light2MProjectile;
   projectileType = Projectile;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	light2Type = "Constantlight2";
	light2Color = "1 1 1";
	light2Radius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = light2AmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 2;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function light2MImage::onFire(%this, %obj, %slot)
{
	if(%obj.lst[%this] > getSimTime())
	{
		return;
	}
	%obj.lst[%this] = getSimTime() + 2000;
	if(%obj.getDatablock().isMage)
	{
		if(%obj.hasEnergy(%this.MPused))
		{
			light2Ray(%obj);
			%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.MPused);
		}
		else if(isObject(%client = %obj.getControllingClient()))
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/" @ %this.MPused, 3);
		}
	}
	else
	{
		light2Ray(%obj);
	}
}

function light2Ray(%obj)
{
	%start = %obj.getEyePoint();
	%end = vectorAdd(%start, vectorScale(%obj.getEyeVector(), 200));
	%typemasks = $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $Typemasks::PlayerObjectType;
	%ray = containerRayCast(%start, %end, %typemasks, %obj);
	if(isObject(%col = firstWord(%ray)))
	{
		%proj = new projectile()
		{
			datablock = light2MProjectile;
			initialPosition = vectorAdd(posFromRaycast(%ray), vectorScale("0 0 50", getWord(%obj.getScale(), 2)));
			initialVelocity = "0 0 -50";
			scale = %obj.getScale();
			sourceObject = %obj;
			sourceSlot = 0;
			client = %obj.client;
		};
		MissionCleanup.add(%proj);
	}
}

function Player::light2Heal(%obj)
{
	if(isObject(%obj) && %obj.getState() !$= "Dead")
	{
		if(%obj.hasEnergy(2))
		{
			%obj.setEnergyLevel(%obj.getEnergyLevel() - 2);
			%scale = getWord(%obj.getScale(), 2);
			InitContainerRadiusSearch(%obj.getPosition(), 3 * %scale, $Typemasks::PlayerObjectType);
			while(isObject(%hit = ContainerSearchNext()))
			{
				if(minigameCanDamage(%obj, %hit))
				{
					%hit.addHealth(1);
					if(isObject(%client = %hit.getControllingClient()))
					{
						%md = %hit.getDatablock().maxDamage;
						%client.bottomPrint("\c2You are being healed. Health: \c3" @ %md - %hit.getDamageLevel() @ "/" @ %md, 3);
					}
				}
			}
			%obj.light2HealSched = %obj.schedule(100, light2Heal);
		}
		else if(isObject(%client = %obj.getControllingClient()) && %obj.getDatablock().isMage)
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/2", 3);
		}
	}
}

package Magiclight2
{
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%this.isMage && %slot == 4 && %obj.getMountedImage(0) == light2MImage.getID())
		{
			if(%val)
			{
				%obj.light2Heal();
			}
			else
			{
				cancel(%obj.light2HealSched);
			}
		}
	}
};
activatePackage(Magiclight2);