datablock AudioProfile(LightExplosionSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/LightExplosion.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock ParticleData(LightAmbientParticle)
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
datablock ParticleEmitterData(LightAmbientEmitter)
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
	particles = LightAmbientParticle;
	uiName = "Light - Ambient";
};

datablock ParticleData(LightTrailParticle)
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
datablock ParticleEmitterData(LightTrailEmitter)
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
	particles = LightTrailParticle;
	uiName = "Light - Trail";
};

datablock ParticleData(LightExplosionParticle)
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
datablock ParticleEmitterData(LightExplosionEmitter)
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
	particles = LightExplosionParticle;
};

datablock ExplosionData(LightMExplosion)
{
	lifeTimeMS = 1000;

	particleEmitter = LightExplosionEmitter;
	particleDensity = 75;
	particleRadius = 3;
	soundProfile = LightExplosionSound;
	
	faceViewer = true;
	explosionScale = "1 1 1";

	emitter[0] = LightExplosionEmitter;
	
	shakeCamera = false;
	camShakeFreq = "1 1 1";
	camShakeAmp = "1 1 1";
	camShakeDuration = 0;
	camShakeRadius = 0;

	lightStartRadius = 20;
	lightEndRadius = 0;
	lightStartColor = "1 1 1";
	lightEndColor = "1 1 1";

	damageRadius = 10;
	radiusDamage = 150;
};

AddDamageType("LightM", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Light> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Light> %1', 0.5, 0);

datablock ProjectileData(LightMProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
	directDamage = 0;
	directDamageType = $DamageType::LightM;
	radiusDamageType = $DamageType::LightM;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;
   brickExplosionForce = 0;
   brickExplosionMaxVolume = 1;
   brickExplosionMaxVolumeFloating = 2;

   impactImpulse = 0;
   verticalImpulse = 0;
   particleEmitter = LightTrailEmitter;
   explosion = LightMExplosion;

   muzzleVelocity = 90;
   velInheritFactor = 1;

   armingDelay = 0;
   lifetime = 4000;
   fadeDelay = 3500;
   bounceElasticity = 0.5;
   bounceFriction = 0.2;
   isBallistic = true;
   gravityMod = 0;

   hasLight = false;
   lightRadius = 10;
   lightColor = "1 1 1";

   uiName = "Light Ray";
};
datablock ItemData(LightMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Light.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Light";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Light";
	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	image = LightMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(LightMImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = LightMItem;
   ammo = " ";
	MPused = 40;
   projectile = LightMProjectile;
   projectileType = Projectile;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	lightType = "ConstantLight";
	lightColor = "1 1 1";
	lightRadius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = LightAmbientEmitter;
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

function LightMImage::onFire(%this, %obj, %slot)
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
			LightRay(%obj);
			%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.MPused);
		}
		else if(isObject(%client = %obj.getControllingClient()))
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/" @ %this.MPused, 3);
		}
	}
	else
	{
		LightRay(%obj);
	}
}

function LightRay(%obj)
{
	%start = %obj.getEyePoint();
	%end = vectorAdd(%start, vectorScale(%obj.getEyeVector(), 200));
	%typemasks = $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $Typemasks::PlayerObjectType;
	%ray = containerRayCast(%start, %end, %typemasks, %obj);
	if(isObject(%col = firstWord(%ray)))
	{
		%proj = new projectile()
		{
			datablock = LightMProjectile;
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

function Player::LightHeal(%obj)
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
			%obj.lightHealSched = %obj.schedule(100, lightHeal);
		}
		else if(isObject(%client = %obj.getControllingClient()) && %obj.getDatablock().isMage)
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/2", 3);
		}
	}
}

package MagicLight
{
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%this.isMage && %slot == 4 && %obj.getMountedImage(0) == LightMImage.getID())
		{
			if(%val)
			{
				%obj.lightHeal();
			}
			else
			{
				cancel(%obj.lightHealSched);
			}
		}
	}
};
activatePackage(MagicLight);