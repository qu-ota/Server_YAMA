datablock AudioProfile(water2FireSound)
{
	fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/water2Loop.wav";
	description = AudioClosestLooping3d;
	preload = true;
};
datablock AudioProfile(water2ExplodeSound)
{
	fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/water2Explode.wav";
	description = AudioClose3d;
	preload = true;
};

datablock ParticleData(water2ExplosionParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 1;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 250;
	textureName = "base/data/particles/bubble";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;

	colors[0] = "0 0 1 1";
	colors[1] = "0 0.4 1 1";
	colors[2] = "0 0.6 1 0";
	sizes[0] = 0.2;
	sizes[1] = 0.35;
	sizes[2] = 0.2;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(water2ExplosionEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 5;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset = 0.1;
	thetaMin = 15;
	thetaMax = 75;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "water2ExplosionParticle";
	uiName = "water2 - Bubbles";
};

datablock ExplosionData(water2MExplosion)
{
	lifeTimeMS = 600;

	particleEmitter = water2ExplosionEmitter;
	particleDensity = 15;
	particleRadius = 0.5;
	soundProfile = water2ExplodeSound;
	
	faceViewer = true;
	explosionScale = "1 1 1";
	
	shakeCamera = false;
	camShakeFreq = "1 1 1";
	camShakeAmp = "1 1 1";
	camShakeDuration = 0;
	camShakeRadius = 0;

	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "1 1 1";
	lightEndColor = "1 1 1";
};

AddDamageType("water2M", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_water2> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_water2> %1', 0.5, 1);

datablock ProjectileData(water2MProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
   directDamage = 3;
	directDamageType = $DamageType::water2M;
	radiusDamageType = $DamageType::water2M;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;
   brickExplosionForce = 0;
   brickExplosionMaxVolume = 1;
   brickExplosionMaxVolumeFloating = 2;

   impactImpulse = 0;
   verticalImpulse = 0;
   explosion = water2MExplosion;
   particleEmitter = water2ExplosionEmitter;

   muzzleVelocity = 20;
   velInheritFactor = 1;

   armingDelay = 0;
   lifetime = 4000;
   fadeDelay = 3500;
   bounceElasticity = 0.5;
   bounceFriction = 0.20;
   isBallistic = true;
   gravityMod = 1;

   hasLight = false;
   lightRadius = 5;
   lightColor = "1 1 1";

   uiName = "water2ball";
};

function water2MProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	parent::damage(%this, %obj, %col, %fade, %pos, %normal);
	%col.setVelocity(vectorAdd(vectorScale(%col.getVelocity(), 0.5), "0 0 1"));
	if(%col.getType() & $Typemasks::PlayerObjectType)
	{
		for(%i = 2; %i < 7; %i++)
		{
			if(%col.getMountedImage(%i) == FireBurnPlayerImage.getID())
			{
				%col.unmountImage(%i);
				break;
			}
		}
	}
}

datablock ItemData(water2MItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/water2.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Water 1";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_water2";
	doColorShift = false;
	colorShiftColor = "0 0.5 1 1";

	image = water2MImage;
	canDrop = false;
};

datablock ShapeBaseImageData(water2MImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = water2MItem;
   ammo = " ";
	MPused = 3;
   projectile = water2MProjectile;
   projectileType = Projectile;
	minShotTime = 99;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.25;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = water2ExplosionEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Fire";
	stateTransitionOnTriggerUp[2]  = "Ready";
	stateTimeoutValue[2]           = 0.1;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = false;
	stateSound[2]                  = water2FireSound;
};

function water2MImage::onFire(%this, %obj, %slot)
{
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

package Magicwater2
{
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%obj.getMountedImage(0) == water2MImage.getID() && %slot == 4 && %val)
		{
			if(%this.isMage && (%obj.lastwater2Spec + 10000) < getSimTime())
			{
				if(%obj.hasEnergy(60))
				{
					%obj.lastwater2Spec = getSimTime();
					%obj.setEnergyLevel(%obj.getEnergyLevel() - 60);
					for(%i = 0; %i < 30; %i++)
					{
						%obj.schedule(30 * %i, spawnProjectile, 60, water2MProjectile, "15 15 15", getWord(%obj.getScale(), 2));
					}
				}
				else if(isObject(%client = %obj.getControllingClient()))
				{
					%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/60", 3);
				}
			}
		}
	}
};
activatePackage(Magicwater2);