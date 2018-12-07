datablock AudioProfile(DarkLoopSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/DarkProjLoop.wav";
   description = AudioCloseLooping3d;
   preload = true;
};
datablock AudioProfile(DarkBlindSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/Blind.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ParticleData(DarkAmbientParticle)
{
	dragCoefficient = 1.75;
	windCoefficient = 0;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 1500;
	lifetimeVarianceMS = 500;
	textureName = "base/data/particles/dot";
	spinSpeed = 0;
	spinRandomMin = -300;
	spinRandomMax = 300;
	useInvAlpha = true;

	colors[0] = "0 0 0 1";
	colors[1] = "0.1 0 0.05 0";
	sizes[0] = 0.2;
	sizes[1] = 0.4;
};
datablock ParticleEmitterData(DarkAmbientEmitter)
{
	ejectionPeriodMS = 15;
	periodVarianceMS = 0;
	ejectionVelocity = 1.5;
	velocityVariance = 1;
	ejectionOffset = 0.0;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = DarkAmbientParticle;

	uiName = "Darkness - Ambient";
};

datablock ParticleData(DarkBlindParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 0;
	inheritedVelFactor = 1;
	constantAcceleration = 0;
	lifetimeMS = 115;
	lifetimeVarianceMS = 15;
	textureName = "base/data/particles/dot";
	spinSpeed = 0;
	spinRandomMin = -100;
	spinRandomMax = 100;
	useInvAlpha = true;

	colors[0] = "0 0 0 1";
	colors[1] = "0.1 0 0.05 0";
	sizes[0] = 1.5;
	sizes[1] = 1;
};
datablock ParticleEmitterData(DarkBlindEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 3;
	velocityVariance = 2.5;
	ejectionOffset = 0.3;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = DarkBlindParticle;
};

datablock ShapeBaseImageData(DarkBlindPlayerImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = $Headslot;
   offset = "0 0 0";
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

	stateName[0]               = "Wait";
	stateTimeoutValue[0]       = 5;
	stateEmitter[0]            = DarkBlindEmitter;
	stateEmitterTime[0]        = 5000;
	stateEmitterTime[0]        = 5;
	stateTransitionOnTimeout[0]= "Dismount";

	stateName[1]               = "Dismount";
	stateScript[1]             = "Dismount";
};

function DarkBlindPlayerImage::onMount(%this, %obj, %slot)
{
	%obj.oldDatablock = %obj.getDatablock();
	%obj.setDatablock(PlayerFPmageArmor);
	serverPlay3d(DarkBlindSound, %obj.getEyePoint());
	parent::onMount(%this, %obj, %slot);
}

function DarkBlindPlayerImage::Dismount(%this, %obj, %slot)
{
	%obj.unmountImage(%slot);
	if(%obj.oldDatablock != PlayerFPmageArmor.getID())
	{
		%obj.setDatablock(%obj.oldDatablock);
	}
}

datablock ExplosionData(DarkMExplosion)
{
	lifeTimeMS = 250;

	particleEmitter = DarkAmbientEmitter;
	particleDensity = 75;
	particleRadius = 1;

	emitter[0] = DarkAmbientEmitter;

	faceViewer = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "30 30 30";
	camShakeAmp = "7 2 7";
	camShakeDuration = 0.6;
	camShakeRadius = 2.5;

	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "1 1 1";
	lightEndColor = "1 1 1";

	uiName = "Darkness";
};

AddDamageType("DarkM", ' %1', '%2 %1', 1, 1);

datablock ProjectileData(DarknessProjectile)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage = 0;
	directDamageType = $DamageType::DarkM;

	impactImpulse = 400;
	verticalImpulse = 400;
	explosion = DarkMExplosion;
	particleEmitter = DarkAmbientEmitter;
	sound = DarkLoopSound;

	muzzleVelocity = 70;
	velInheritFactor = 0;

	armingDelay = 0;
	lifetime = 4000;
	fadeDelay = 4000;
	bounceElasticity = 0.5;
	bounceFriction = 0.5;
	isBallistic = false;

	hasLight = false;
	lightRadius = 1;
	lightColor = "0 0 0";

	uiName = "Dark Ball";
};

function DarknessProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	if(%col.getType() & $Typemasks::PlayerObjectType)
	{
		if(%col.getMountedImage(0) == LightMImage.getID())
		{
			return;
		}
		if((%col.lastBlind + 10000) < getSimTime())
		{
			%col.lastBlind = getSimTime();
			for(%i = 2; %i < 7; %i++)
			{
				%img = %col.getMountedImage(%i);
				if(%img == DarkBlindPlayerImage.getID())
				{
					return;
				}
				if(%img < 1)
				{
					%col.mountImage(DarkBlindPlayerImage, %i);
					return;
				}
			}
		}
	}
}

datablock ItemData(DarkMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Dark.dts";
	rotate = false;
	mass = 0.2;
	density = 0.1;
	elasticity = 0.8;
	friction = 0.2;
	emap = true;

	uiName = "Magic - Darkness";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Darkness";
	doColorShift = false;
	colorShiftColor = "0 0 0 1";

	image = DarkMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(DarkMImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = "0 0 0";

	correctMuzzleVector = true;

	className = "WeaponImage";
	item = DarkMItem;
	ammo = " ";
	MPused = 30;
	projectile = DarknessProjectile;
	projectileType = Projectile;

	melee = false;
	armReady = true;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateSequence[1]               = "Ready";
	stateEmitter[1]                = DarkAmbientEmitter;
	stateEmitterTime[1]            = 600;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 1;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function DarkMImage::onFire(%this, %obj, %slot)
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

package MagicDark
{
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%obj.getMountedImage(0) == DarkMImage.getID() && %slot == 4 && %val)
		{
			if(%this.isMage && (%obj.lastDarkSpec + 10000) < getSimTime())
			{
				if(%obj.hasEnergy(60))
				{
					%obj.lastDarkSpec = getSimTime();
					%obj.setEnergyLevel(%obj.getEnergyLevel() - 60);
					%start = %obj.getEyePoint();
					%end = vectorAdd(%start, vectorScale(%obj.getEyeVector(), 200));
					%ray = containerRaycast(%start, %end, $Typemasks::PlayerObjectType | $Typemasks::FxBrickObjectType, %obj);
					if(isObject(%hit = firstWord(%ray)) && %hit.getType() & $Typemasks::PlayerObjectType)
					{
						if(minigameCanDamage(%obj, %hit) && getMinigameFromObject(%hit).weaponDamage)
						{
							DarknessProjectile.damage(%obj, %hit, 0, "0 0 0", "0 0 1");
							%hT = %hit.getTransform();
							%oT = %obj.getTransform();
							%obj.setTransform(%hT);
							%hit.setTransform(%oT);
							%obj.spawnExplosion(DarknessProjectile, getWord(%obj.getScale(), 2));
							%hit.spawnExplosion(DarknessProjectile, getWord(%hit.getScale(), 2));
						}
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
activatePackage(MagicDark);