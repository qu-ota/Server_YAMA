datablock AudioProfile(WindLoopSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/WindTravel.wav";
   description = AudioCloseLooping3d;
   preload = true;
};
datablock AudioProfile(WindExplosionSound)
{
   fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/WindExplosion.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ParticleData(WindAmbientParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.2;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 400;
	lifetimeVarianceMS = 100;
	textureName = "base/data/particles/cloud";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;

	colors[0] = "0.33 0.33 0.33 1";
	colors[1] = "0.67 0.67 0.67 0.8";
	colors[2] = "1 1 1 0.2";
	sizes[0] = 0.4;
	sizes[1] = 0.3;
	sizes[2] = 0.2;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(WindAmbientEmitter)
{
	ejectionPeriodMS = 7;
	periodVarianceMS = 0;
	ejectionVelocity = 0.5;
	velocityVariance = 0;
	ejectionOffset = 0.2;
	thetaMin = 25;
	thetaMax = 155;
	phiReferenceVel = 360;
	phiVariance = 5;
	overrideAdvance = false;
	particles = "WindAmbientParticle";
	uiName = "Wind - Ambient";
};
datablock ParticleData(WindTrailParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.2;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 400;
	lifetimeVarianceMS = 100;
	textureName = "base/data/particles/cloud";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = false;

	colors[0] = "0.33 0.33 0.33 1";
	colors[1] = "0.67 0.67 0.67 0.8";
	colors[2] = "1 1 1 0.2";
	sizes[0] = 0.4;
	sizes[1] = 0.25;
	sizes[2] = 0.1;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(WindTrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset   = 1.0;
	thetaMin         = 0;
	thetaMax         = 40;
	phiReferenceVel  = 720;
	phiVariance      = 10;
	overrideAdvance = false;
	particles = "WindAmbientParticle";
	uiName = "Wind - Trail";
};

datablock ParticleData(WindExplosionParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.2;
	constantAcceleration = 0;
	lifetimeMS = 750;
	lifetimeVarianceMS = 250;
	textureName = "base/data/particles/cloud";
	spinSpeed = 0;
	spinRandomMin = -1000;
	spinRandomMax = 1000;
	useInvAlpha = false;

	colors[0] = "0.33 0.33 0.33 1";
	colors[1] = "0.67 0.67 0.67 0.8";
	colors[2] = "1 1 1 0.2";
	sizes[0] = 2;
	sizes[1] = 1.8;
	sizes[2] = 1.6;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(WindExplosionEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 4;
	ejectionVelocity = 8;
	velocityVariance = 0.2;
	ejectionOffset = 1;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 720;
	phiVariance = 20;
	overrideAdvance = false;
	particles = "WindExplosionParticle";
};

datablock ExplosionData(WindMExplosion)
{
	lifeTimeMS = 600;

	particleEmitter = WindExplosionEmitter;
	particleDensity = 15;
	particleRadius = 0.5;

	soundProfile = WindExplosionSound;
	
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

	damageRadius = 1;
	radiusDamage = 5;
};

AddDamageType("WindM", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Wind> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Wind> %1', 0.5, 0);

datablock ProjectileData(WindMProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
	radiusDamageType = $DamageType::WindM;
	sound = WindLoopSound;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;
   brickExplosionForce = 0;
   brickExplosionMaxVolume = 1;
   brickExplosionMaxVolumeFloating = 2;

   impactImpulse = 0;
   verticalImpulse = 0;
   explosion = WindMExplosion;
   particleEmitter = WindTrailEmitter;

   muzzleVelocity = 50;
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

   uiName = "Windblast";
};

function WindMProjectile::RadiusDamage(%this, %obj, %col, %dist, %pos, %amt)
{
	parent::RadiusDamage(%this, %obj, %col, %dist, %pos, %amt);
	if(isObject(%col) && %col.getType() & $Typemasks::PlayerObjectType)
	{
		%col.setVelocity(vectorAdd(%col.getVelocity(), vectorScale(setWord(vectorScale(%obj.getVelocity(), getWord(%obj.getScale(), 2)), 2, "10"), 0.2)));
	}
}

datablock ItemData(WindMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Wind.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Wind";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Wind";
	doColorShift = false;
	colorShiftColor = "0.5 0.5 1 1";

	image = WindMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(WindMImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = WindMItem;
   ammo = " ";
	MPused = 30;
   projectile = WindMProjectile;
   projectileType = Projectile;
	minShotTime = 500;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = WindAmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Ready";
	stateTimeoutValue[2]           = 0.5;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateSequence[2]               = "Water";
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function WindMImage::onFire(%this, %obj, %slot)
{
	if(%obj.lst[%this] > getSimTime())
	{
		return;
	}
	%obj.lst[%this] = getSimTime() + 500;
	if(%obj.getDatablock().isMage)
	{
		if(%obj.hasEnergy(%this.MPused))
		{
			parent::onFire(%this, %obj, %slot);
			%obj.setVelocity(vectorAdd(%obj.getVelocity(), vectorScale(%obj.getForwardVector(), -5)));
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
		%obj.setVelocity(vectorAdd(%obj.getVelocity(), vectorScale(%obj.getForwardVector(), -3)));
	}
}

package MagicWind
{
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%obj.getMountedImage(0) == WindMImage.getID() && %val)
		{
			if(%slot == 2 && !%obj.isOnGround() && ((%obj.lastDoubleJump + 2000) < getSimTime()))
			{
				%obj.lastDoubleJump = getSimTime();
				%scale = getWord(%obj.getScale(), 2) / 2;
				%vel = vectorScale("0 0 25", %scale);
				if(%this.isMage)
				{
					if(%obj.hasEnergy(10))
					{
						%obj.addVelocity(%vel);
						%obj.setEnergyLevel(%obj.getEnergyLevel() - 10);
						%obj.spawnExplosion(WindMProjectile, %scale);
					}
					else if(isObject(%client = %obj.getControllingClient()))
					{
						%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/10", 3);
					}
				}
				else
				{
					%obj.addVelocity(%vel);
					%obj.spawnExplosion(WindMProjectile, %scale);
				}
			}
			if(%slot == 4 && ((%obj.lastDownBlast + 2000) < getSimTime()))
			{
				if(%this.isMage)
				{
					if(%obj.hasEnergy(50))
					{
						%obj.lastDownBlast = getSimTime();
						%obj.setEnergyLevel(%obj.getEnergyLevel() - 50);
						%scale = getWord(%obj.getScale(), 2);
						InitContainerRadiusSearch(%obj.getPosition(), 10 * %scale, $Typemasks::PlayerObjectType);
						while(isObject(%hit = ContainerSearchNext()))
						{
							if(minigameCanDamage(%obj, %hit) && getMinigameFromObject(%hit).weaponDamage && !%hit.isOnGround())
							{
								%hit.damage(%obj, %hit.getPosition(), 10 * %scale, $DamageType::WindM);
								%hit.lastPusher = %obj.client;
								%hit.lastPushTime = getSimTime();
								%add = vectorScale("0 0 -30", %scale);
								%hit.setVelocity(vectorAdd(%hit.getVelocity(), %add));
							}
						}
					}
					else if(isObject(%client = %obj.getControllingClient()))
					{
						%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/50", 3);
					}
				}
			}
		}
	}
};
activatePackage(MagicWind);