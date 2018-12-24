datablock AudioProfile(Fire4ShotSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/Fire4Shot.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(Fire4LoopSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/Fire4Loop.wav";
   description = AudioCloseLooping3d;
   preload = true;
};

datablock ParticleData(Fire4AmbientParticle)
{
	dragCoefficient = 1;
	windCoefficient = 1;
	gravityCoefficient = -0.4;
	inheritedVelFactor = 0.5;
	constantAcceleration = 0;
	lifetimeMS = 2000;
	lifetimeVarianceMS = 500;
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;
	framesPerSec = 1;
	textureName = "base/data/particles/cloud";

	colors[0] = "1 0 0 1";
	colors[1] = "1 0.33 0 0.8";
	colors[2] = "1 0.67 0 0.5";
	colors[3] = "1 1 0 0.2";

	sizes[0] = 0.6;
	sizes[1] = 0.4;
	sizes[2] = 0.3;
	sizes[3] = 0.2;

	times[0] = 0;
	times[1] = 0.3;
	times[2] = 0.6;
	times[3] = 1;
};
datablock ParticleEmitterData(Fire4AmbientEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 5;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset = 0;
	thetaMin = 45;
	thetaMax = 135;
	phiReferenceVel = 0;
	phiVariance = 360;
	particles = Fire4AmbientParticle;
	lifetimeMS = 0;
	lifetimeVarianceMS = 0;
	uiName = "Fire4 - Ambient";
};

datablock ParticleData(Fire4ExplosionParticle)
{
	dragCoefficient = 1;
	gravityCoefficient = -0.4;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 650;
	lifetimeVarianceMS = 350;
	textureName = "base/data/particles/cloud";
	spinSpeed = 10;
	spinRandomMin = -50;
	spinRandomMax = 50;

	colors[0] = "0.67 0 0 0.9";
	colors[1] = "0.67 0.2 0 0.8";
	colors[2] = "0.67 0.27 0.13 0.5";
	colors[3] = "0 0 0 0";

	sizes[0] = 1;
	sizes[1] = 1.5;
	sizes[2] = 2;
	sizes[3] = 2.5;

	times[0] = 0;
	times[1] = 0.3;
	times[2] = 0.6;
	times[3] = 1;

	useInvAlpha = false;
};
datablock ParticleEmitterData(Fire4ExplosionEmitter)
{
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 1.0;
	ejectionOffset = 0.5;
	thetaMin = 0;
	thetaMax = 80;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "Fire4ExplosionParticle";
	uiName = "Fire4 - Explosion";
};

datablock ExplosionData(Fire4MExplosion)
{
	lifeTimeMS = 600;

	particleEmitter = Fire4ExplosionEmitter;
	particleDensity = 25;
	particleRadius = 0.5;
	soundProfile = Fire4ShotSound;
	
	faceViewer = true;
	explosionScale = "1 1 1";
	
	shakeCamera = false;
	camShakeFreq = "1 1 1";
	camShakeAmp = "1 1 1";
	camShakeDuration = 0;
	camShakeRadius = 0;

	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1 0 0";
	lightEndColor = "1 1 0";

	radiusDamage = 10;
	damageRadius = 1;
};

AddDamageType("Fire4M", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Fire4> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Fire4> %1', 0.25, 0);

datablock ProjectileData(Fire4MProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
   directDamage = 10;
	radiusDamageType = $DamageType::Fire4M;

   brickExplosionRadius = 1;
   brickExplosionImpact = true;
   brickExplosionForce = 20;
   brickExplosionMaxVolume = 10;
   brickExplosionMaxVolumeFloating = 15;

   impactImpulse = 600;
   verticalImpulse = 400;
   explosion = Fire4MExplosion;
   particleEmitter = Fire4AmbientEmitter;
	sound = Fire4LoopSound;

   muzzleVelocity = 20;
   velInheritFactor = 1;

   armingDelay = 0;
   lifetime = 4000;
   fadeDelay = 3500;
   bounceElasticity = 0.5;
   bounceFriction = 0.20;
   isBallistic = true;
   gravityMod = 0.5;

   hasLight = true;
   lightRadius = 5;
   lightColor = "1 0.5 0";

   uiName = "Fire4ball";
};

function Fire4MProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if(%col.getClassName() $= "TSStatic")
	{
		if(%col.shapeName $= "Add-Ons/Weapon_ElementalSpells/OtherShapes/IceBlock.dts")
		{
			if(isObject(%col.player))
			{
				%col.player.thaw();
			}
			%col.delete();
		}
	}
	parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);
}

function Fire4MProjectile::RadiusDamage(%this, %obj, %col, %distance, %pos, %amt)
{
	if(!isObject(%col))
	{
		return;
	}
	%col.damage(%obj, %pos, 6 * getWord(%col.getScale(), 1), $DamageType::Fire4M);
	if(%col.getType() & $Typemasks::PlayerObjectType && %col.getMountedImage(0) != WaterMImage.getID())
	{
		%col.burnCount = 0;
		if(isObject(%source = %obj.sourceObject))
		{
			%col.lastBurner = %source;
		}
		else
		{
			%col.lastBurner = %obj;
		}
		for(%i = 1; %i < 7; %i++)
		{
			%img = %col.getMountedImage(%i);
			if(%img == Fire4BurnPlayerImage.getID())
			{
				return;
			}
			if(%img < 1)
			{
				%col.mountImage(Fire4BurnPlayerImage, %i);
				return;
			}
		}
	}
}

datablock ShapeBaseImageData(Fire4BurnPlayerImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 2;
   offset = "0 0 -0.5";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = "";
   ammo = " ";
   projectile = "";
   projectileType = Projectile;

   melee = false;
   armReady = false;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	lightType = "ConstantLight";
	lightColor = "1 0.55 0";
	lightRadius = 4;

	stateName[0]                   = "Wait";
	stateTimeoutValue[0]           = 0.9;
	stateEmitter[0]                = Fire4AmbientEmitter;
	stateEmitterTime[0]            = 1;
	stateTransitionOnTimeout[0]    = "Burn";
	stateSound[0]                  = Fire4LoopSound;

	stateName[1]                   = "Burn";
	stateScript[1]                 = "Damage";
	stateEmitter[1]                = Fire4ExplosionEmitter;
	stateEmitterTime[1]            = 0.1;
	stateTimeoutValue[1]           = 0.1;
	stateTransitionOnTimeout[1]    = "Wait";
	stateSound[1]                  = Fire4ShotSound;
};

function Fire4BurnPlayerImage::Damage(%this, %obj, %slot)
{
	if(%obj.getWaterCoverage() > 1 || %obj.getMountedImage(0) == WaterMImage.getID())
	{
		%obj.unmountImage(%slot);
		return;
	}
	if(%obj.getState() $= "Dead")
	{
		return;
	}
	%obj.damage(%obj.lastBurner, %obj.getPosition(), (6 - %obj.burnCount) * getWord(%obj.scale, 1), $DamageType::Fire4M);
	%obj.burnCount++;
	if((%obj.burnCount >= 4 && !%obj.Fire4Special) || (%obj.burnCount >= 6 && %obj.Fire4Special))
	{
		%obj.unmountImage(%slot);
	}
}

function Fire4BurnPlayerImage::onUnmount(%this, %obj, %slot)
{
	%obj.burnCount = 0;
	%obj.lastBurner = 0;
	%obj.Fire4Special = false;
}
function Fire4BurnPlayerImage::onMount(%this, %obj, %slot)
{
	%obj.burnCount = 0;
}

datablock ItemData(Fire4MItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Fire4.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Fire 3";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Fire4";
	doColorShift = false;
	colorShiftColor = "1 0.25 0 1";

	image = Fire4MImage;
	canDrop = false;
};

datablock ShapeBaseImageData(Fire4MImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";
	firstPersonParticles = false;

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = Fire4MItem;
   ammo = " ";
	MPused = 25;
   projectile = Fire4MProjectile;
   projectileType = Projectile;
	minShotTime = 750;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	lightType = "ConstantLight";
	lightColor = "1 0.35 0";
	lightRadius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 1.0;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire4";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = Fire4AmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire4";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 2.5;
	stateFire4[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateSequence[2]               = "Fire4";
	stateScript[2]                 = "onfire";
	stateWaitForTimeout[2]         = true;
	stateSound[2]                  = Fire4ShotSound;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function Fire4MImage::onfire(%this, %obj, %slot)
{
	if(%obj.lst[%this] > getSimTime())
	{
		return;
	}
	%obj.lst[%this] = getSimTime() + 750;
	if(%obj.getDatablock().isMage)
	{
		if(%obj.hasEnergy(%this.MPused))
		{
			parent::onfire(%this, %obj, %slot);
			%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.MPused);
		}
		else if(isObject(%client = %obj.getControllingClient()))
		{
			%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/" @ %this.MPused, 3);
		}
	}
	else
	{
		parent::onfire(%this, %obj, %slot);
	}
}

package MagicFire4
{
	function armor::onEnterLiquid(%this, %obj, %coverage, %type)
	{
		if(%type < 4) //Some sort of water
		{
			for(%i = 2; %i < 7; %i++)
			{
				if(%obj.getMountedImage(%i) == Fire4BurnPlayerImage.getID())
				{
					%obj.unmountImage(%i);
				}
			}
		}
		parent::onEnterLiquid(%this, %obj, %coverage, %type);
	}
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%this.isMage && %obj.getMountedImage(0) == Fire4MImage.getID() && %slot == 4 && %val)
		{
			if(%obj.hasEnergy(100))
			{
				%obj.setEnergyLevel(%obj.getEnergyLevel() - 100);
				%obj.Fire4Special = true;
				%obj.lastBurner = %obj;
				for(%i = 2; %i < 7; %i++)
				{
					%img = %obj.getMountedImage(%i);
					if(%img == Fire4BurnPlayerImage.getID())
					{
						return;
					}
					if(%img < 1)
					{
						%obj.mountImage(Fire4BurnPlayerImage, %i);
						return;
					}
				}
			}
			else if(isObject(%client = %obj.getControllingClient()))
			{
				%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/100", 3);
			}
		}
	}
	function armor::onCollision(%this, %obj, %col, %fade, %pos, %normal)
	{
		parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);
		if(%col.Fire4Special && (%mini = getMinigameFromObject(%obj)) == getMinigameFromObject(%col) && minigameCanDamage(%obj, %col) && %mini.weaponDamage)
		{
			%obj.damage(%col, %pos, 5 * getWord(%col.getScale(), 0.5), $DamageType::Fire4M);
			%obj.lastBurner = %col;
			for(%i = 2; %i < 7; %i++)
			{
				%img = %obj.getMountedImage(%i);
				if(%img == Fire4BurnPlayerImage.getID())
				{
					%obj.burnCount = 0;
					return;
				}
				if(%img < 1)
				{
					%obj.mountImage(Fire4BurnPlayerImage, %i);
					return;
				}
			}
		}
	}
};
activatePackage(MagicFire4);