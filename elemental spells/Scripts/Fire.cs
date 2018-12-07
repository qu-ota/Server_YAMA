datablock AudioProfile(FireShotSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/FireShot.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(FireLoopSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/FireLoop.wav";
   description = AudioCloseLooping3d;
   preload = true;
};

datablock ParticleData(FireAmbientParticle)
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
datablock ParticleEmitterData(FireAmbientEmitter)
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
	particles = FireAmbientParticle;
	lifetimeMS = 0;
	lifetimeVarianceMS = 0;
	uiName = "Fire - Ambient";
};

datablock ParticleData(FireExplosionParticle)
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
datablock ParticleEmitterData(FireExplosionEmitter)
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
	particles = "FireExplosionParticle";
	uiName = "Fire - Explosion";
};

datablock ExplosionData(FireMExplosion)
{
	lifeTimeMS = 600;

	particleEmitter = FireExplosionEmitter;
	particleDensity = 25;
	particleRadius = 0.5;
	soundProfile = FireShotSound;
	
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

	radiusDamage = 1;
	damageRadius = 1;
};

AddDamageType("FireM", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Fire> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Fire> %1', 0.25, 0);

datablock ProjectileData(FireMProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
   directDamage = 5;
	radiusDamageType = $DamageType::FireM;

   brickExplosionRadius = 2;
   brickExplosionImpact = true;
   brickExplosionForce = 20;
   brickExplosionMaxVolume = 10;
   brickExplosionMaxVolumeFloating = 15;

   impactImpulse = 600;
   verticalImpulse = 400;
   explosion = FireMExplosion;
   particleEmitter = FireAmbientEmitter;
	sound = FireLoopSound;

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

   uiName = "Fireball";
};

function FireMProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
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

function FireMProjectile::RadiusDamage(%this, %obj, %col, %distance, %pos, %amt)
{
	if(!isObject(%col))
	{
		return;
	}
	%col.damage(%obj, %pos, 15 * getWord(%col.getScale(), 2), $DamageType::FireM);
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
		for(%i = 2; %i < 7; %i++)
		{
			%img = %col.getMountedImage(%i);
			if(%img == FireBurnPlayerImage.getID())
			{
				return;
			}
			if(%img < 1)
			{
				%col.mountImage(FireBurnPlayerImage, %i);
				return;
			}
		}
	}
}

datablock ShapeBaseImageData(FireBurnPlayerImage)
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
	stateEmitter[0]                = FireAmbientEmitter;
	stateEmitterTime[0]            = 1;
	stateTransitionOnTimeout[0]    = "Burn";
	stateSound[0]                  = FireLoopSound;

	stateName[1]                   = "Burn";
	stateScript[1]                 = "Damage";
	stateEmitter[1]                = FireExplosionEmitter;
	stateEmitterTime[1]            = 0.1;
	stateTimeoutValue[1]           = 0.1;
	stateTransitionOnTimeout[1]    = "Wait";
	stateSound[1]                  = FireShotSound;
};

function FireBurnPlayerImage::Damage(%this, %obj, %slot)
{
	if(%obj.getWaterCoverage() > 0.5 || %obj.getMountedImage(0) == WaterMImage.getID())
	{
		%obj.unmountImage(%slot);
		return;
	}
	if(%obj.getState() $= "Dead")
	{
		return;
	}
	%obj.damage(%obj.lastBurner, %obj.getPosition(), (10 - %obj.burnCount) * getWord(%obj.scale, 2), $DamageType::FireM);
	%obj.burnCount++;
	if((%obj.burnCount >= 5 && !%obj.fireSpecial) || (%obj.burnCount >= 10 && %obj.fireSpecial))
	{
		%obj.unmountImage(%slot);
	}
}

function FireBurnPlayerImage::onUnmount(%this, %obj, %slot)
{
	%obj.burnCount = 0;
	%obj.lastBurner = 0;
	%obj.fireSpecial = false;
}
function FireBurnPlayerImage::onMount(%this, %obj, %slot)
{
	%obj.burnCount = 0;
}

datablock ItemData(FireMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Fire.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Fire";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Fire";
	doColorShift = false;
	colorShiftColor = "1 0.25 0 1";

	image = FireMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(FireMImage)
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

   item = FireMItem;
   ammo = " ";
	MPused = 25;
   projectile = FireMProjectile;
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
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = FireAmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 2.50;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateSequence[2]               = "Fire";
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;
	stateSound[2]                  = FireShotSound;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function FireMImage::onFire(%this, %obj, %slot)
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

package MagicFire
{
	function armor::onEnterLiquid(%this, %obj, %coverage, %type)
	{
		if(%type < 4) //Some sort of water
		{
			for(%i = 2; %i < 7; %i++)
			{
				if(%obj.getMountedImage(%i) == FireBurnPlayerImage.getID())
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
		if(%this.isMage && %obj.getMountedImage(0) == FireMImage.getID() && %slot == 4 && %val)
		{
			if(%obj.hasEnergy(100))
			{
				%obj.setEnergyLevel(%obj.getEnergyLevel() - 100);
				%obj.fireSpecial = true;
				%obj.lastBurner = %obj;
				for(%i = 2; %i < 7; %i++)
				{
					%img = %obj.getMountedImage(%i);
					if(%img == FireBurnPlayerImage.getID())
					{
						return;
					}
					if(%img < 1)
					{
						%obj.mountImage(FireBurnPlayerImage, %i);
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
		if(%col.fireSpecial && (%mini = getMinigameFromObject(%obj)) == getMinigameFromObject(%col) && minigameCanDamage(%obj, %col) && %mini.weaponDamage)
		{
			%obj.damage(%col, %pos, 10 * getWord(%col.getScale(), 2), $DamageType::FireM);
			%obj.lastBurner = %col;
			for(%i = 2; %i < 7; %i++)
			{
				%img = %obj.getMountedImage(%i);
				if(%img == FireBurnPlayerImage.getID())
				{
					%obj.burnCount = 0;
					return;
				}
				if(%img < 1)
				{
					%obj.mountImage(FireBurnPlayerImage, %i);
					return;
				}
			}
		}
	}
};
activatePackage(MagicFire);