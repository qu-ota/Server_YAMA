datablock audioProfile(IceExplodeSound)
{
	fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/IceExplode.wav";
	description = AudioClose3D;
	preload = true;
};

datablock ParticleData(IceAmbientParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.5;
	gravityCoefficient = 0.1;
	inheritedVelFactor = 1;
	constantAcceleration = 0;
	lifetimeMS = 2500;
	lifetimeVarianceMS = 500;
	spinSpeed = 0;
	spinRandomMin = -200;
	spinRandomMax = 200;
	useInvAlpha = false;
	textureName = "Add-Ons/Weapon_ElementalSpells/OtherShapes/snow.png";

	colors[0] = "1 1 1 0";
	colors[1] = "1 1 1 1";
	colors[2] = "0.5 1 1 0.8";
	colors[3] = "0 1 1 0";
	sizes[0] = 0.7;
	sizes[1] = 0.5;
	sizes[2] = 0.3;
	sizes[3] = 0.3;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 0.8;
	times[3] = 1;
};
datablock ParticleEmitterData(IceAmbientEmitter)
{
	ejectionPeriodMS = 75;
	periodVarianceMS = 25;
	ejectionVelocity = 0.6;
	velocityVariance = 0.5;
	ejectionOffset = 0.5;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	particles = IceAmbientParticle;
	lifetimeMS = 0;
	lifetimeVarianceMS = 0;
	uiName = "Ice - Snow A";
};

datablock ParticleData(IceTrailParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.5;
	gravityCoefficient = 0.25;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 500;
	spinSpeed = 0;
	spinRandomMin = -400;
	spinRandomMax = 400;
	useInvAlpha = false;
	textureName = "Add-Ons/Weapon_ElementalSpells/OtherShapes/snow.png";

	colors[0] = "1 1 1 0";
	colors[1] = "1 1 1 1";
	colors[2] = "0.5 1 1 0.8";
	colors[3] = "0 1 1 0";
	sizes[0] = 0.7;
	sizes[1] = 0.5;
	sizes[2] = 0.3;
	sizes[3] = 0.3;
	times[0] = 0;
	times[1] = 0.1;
	times[2] = 0.8;
	times[3] = 1;
};
datablock ParticleEmitterData(IceTrailEmitter)
{
	ejectionPeriodMS = 8;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset = 0;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	particles = IceTrailParticle;
	lifetimeMS = 0;
	lifetimeVarianceMS = 0;
	uiName = "Ice - Snow B";
};

datablock ParticleData(IceExplosionParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0.5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 5000;
	lifetimeVarianceMS = 1000;
	textureName = "base/data/particles/cloud";
	spinSpeed = 10;
	spinRandomMin = -50;
	spinRandomMax = 50;
	useInvAlpha = true;

	colors[0] = "1 1 1 0";
	colors[1] = "1 1 1 1";
	colors[2] = "0.5 1 1 0";
	sizes[0] = 1.5;
	sizes[1] = 2;
	sizes[2] = 3;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 1;
};
datablock ParticleEmitterData(IceExplosionEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 5;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset = 0.5;
	thetaMin = 15;
	thetaMax = 75;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "IceExplosionParticle";
	uiName = "Ice - Fog";
};

datablock ExplosionData(IceMExplosion)
{
	lifeTimeMS = 600;

	emitter[0] = IceTrailEmitter;

	particleEmitter = IceExplosionEmitter;
	particleDensity = 15;
	particleRadius = 1;
	soundProfile = IceExplodeSound;
	
	faceViewer = true;
	explosionScale = "1 1 1";
	
	shakeCamera = false;
	camShakeFreq = "1 1 1";
	camShakeAmp = "1 1 1";
	camShakeDuration = 0;
	camShakeRadius = 0;

	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1 1 1";
	lightEndColor = "0 1 1";
	
	damageRadius = 5;
	radiusDamage = 15;
};

AddDamageType("IceM", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Ice> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Ice> %1', 0.5, 0);

datablock ProjectileData(IceMProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
	directDamage = 5;
	directDamageType = $DamageType::IceM;
	radiusDamageType = $DamageType::IceM;

	brickExplosionRadius = 0;
	brickExplosionImpact = true;
	brickExplosionForce = 0;
	brickExplosionMaxVolume = 1;
	brickExplosionMaxVolumeFloating = 2;

	impactImpulse = 600;
	verticalImpulse = 400;
	explosion = IceMExplosion;
	particleEmitter = IceTrailEmitter;
	sound = WindLoopSound;

	muzzleVelocity = 60;
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
	lightColor = "0.5 1 1";

	uiName = "Iceball";
};

datablock StaticShapeData(IceBlockData)
{
	category = "Statics";
	shapeFile = "Add-Ons/Weapon_ElementalSpells/OtherShapes/IceBlock.dts";
	skinName = 'null';
};

function IceMProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	if(%col.getType() & $Typemasks::PlayerObjectType && (%col.lastFreezeTime + 10000) < getSimTime() && %col.getMountedImage(0) != FireMImage.getID())
	{
		%col.lastFreezeTime = getSimTime();
		%col.setVelocity("0 0 0");
		//Variables for thaw function
		%col.thawData = %col.getDatablock();
		%col.thawImg0 = %col.getMountedImage(0);
		%col.thawImg1 = %col.getMountedImage(1);
		%col.thawClient = %col.getControllingClient();
		if(!isObject(%col.client))
		{
			%col.thawSched = %col.schedule(5000, thaw);
		}
		else
		{
			%col.client.thawSched = %col.client.schedule(5000, thaw);
		}
		//Stop player from being able to do anything in case they're a bot
		%col.unmountImage(0);
		%col.unmountImage(1);
		%col.setDatablock(PlayerNoMoveArmor);
		if(isObject(%client = %col.getControllingClient()) && isObject(%client.camera))
		{
			%client.camera.frozen = true;
			%client.camera.setMode("Corpse", %col);
			%client.setControlObject(%client.camera);
		}
		%scale = %col.getScale();
		%nPos = %col.getPosition();
		%rot = rotFromTransform(%col.getTransform());
		%block = new TSstatic()
		{
			datablock = IceBlockData;
			shapeName = "Add-Ons/Weapon_ElementalSpells/OtherShapes/IceBlock.dts";
			position = %nPos;
			rotation = %rot;
			scale = %scale;
			player = %col;
		};
		MissionCleanup.add(%block);
		%block.schedule(5000, spawnIceExplosion, %block, %nPos, getWord(%scale, 2));
		%block.schedule(5000, delete);
	}
}

function GameConnection::Thaw(%client)
{
	if(isObject(%client.camera) && %client.getControlObject() == %client.camera || !isObject(%client.getControlObject()))
	{
		%client.camera.unmountImage(0);
		%client.camera.frozen = false;
	}
	if(isObject(%obj = %client.player))
	{
		if(%obj.getDamagePercent() < 1)
		{
			%client.setControlObject(%obj);
			%obj.setDatablock(%obj.thawData);
		}
		for(%i = 0; %i < 1; %i++)
		{
			if(isObject(%obj.thawImg[%i]))
			{
				%obj.mountImage(%obj.thawImg[%i], %i);
			}
		}
	}
}
function Player::Thaw(%obj)
{
	%obj.setDatablock(%obj.thawData);
	for(%i = 0; %i < 1; %i++)
	{
		if(isObject(%obj.thawImg[%i]))
		{
			%obj.mountImage(%obj.thawImg[%i], %i);
		}
	}
	if(isObject(%obj.thawClient.camera) && %obj.thawClient.getControlObject() == %obj.thawClient.camera)
	{
		%obj.thawClient.camera.unmountImage(0); //Just in case they were flying around
		%obj.thawClient.camera.frozen = false;
		%obj.thawClient.setControlObject(%obj);
	}
}

function TSstatic::SpawnIceExplosion(%obj, %pos, %scale)
{
	%proj = new projectile()
	{
		datablock = IceMProjectile;
		initialPosition = vectorAdd(%pos, "0 0" @ %scale);
		initialVelocity = "0 0 5";
		scale = %scale SPC %scale SPC %scale;
		sourceObject = "";
		sourceSlot = 0;
		client = "";
		hot = true;
	};
	%proj.schedule(33, explode);
}

function IceMProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if(!%obj.hot)
	{
		parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);
		if(%col.getType() & $TypeMasks::InteriorObjectType || %col.getType() & $TypeMasks::FxBrickObjectType || %col.getType() & $TypeMasks::TerrainObjectType)
		{
			%scale = getWord(%obj.getScale(), 2);
			%block = new TSstatic()
			{
				datablock = IceBlockData;
				shapeName = "Add-Ons/Weapon_ElementalSpells/OtherShapes/IceBlock.dts";
				position = %pos;
				rotation = "0 0 0 0";
				scale = %scale SPC %scale SPC %scale;
			};
			MissionCleanup.add(%block);
			%block.schedule(5000, 0, spawnIceExplosion, %block, %pos, %scale);
			%block.schedule(5000, delete);
		}
	}
}

datablock ItemData(IceMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Ice.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Ice";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Ice";
	doColorShift = false;
	colorShiftColor = "0.5 1 1 1";

	image = IceMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(IceMImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = "0 0 0";

	correctMuzzleVector = true;

	className = "WeaponImage";

	item = IceMItem;
	ammo = " ";
	MPused = 30;
	projectile = IceMProjectile;
	projectileType = Projectile;
	minShotTime = 750;

	melee = false;
	armReady = true;

	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	lightType = "ConstantLight";
	lightColor = "0.5 1 1";
	lightRadius = 5;

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = IceAmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 0.75;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateSequence[2]               = "Ice";
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function IceMImage::onFire(%this, %obj, %slot)
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

package MagicIce
{
	function player::mountImage(%obj, %image, %slot)
	{
		if(%obj.getDatablock() == PlayerNoMoveArmor.getID())
		{
			return;
		}
		parent::mountImage(%obj, %image, %slot);
	}
	function observer::onTrigger(%this, %obj, %slot, %val)
	{
		if(%obj.frozen)
		{
			return;
		}
		parent::onTrigger(%this, %obj, %slot, %val);
	}
	function armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%this.isMage && %obj.getMountedImage(0) == IceMImage.getID() && %val && %slot == 4 && (%obj.lastMassFreeze + 10000) < getSimTime())
		{
			if(%obj.hasEnergy(75))
			{
				%obj.lastMassFreeze = getSimTime();
				%obj.setEnergyLevel(%obj.getEnergyLevel() - 75);
				%scale = getWord(%obj.getScale(), 2);
				InitContainerRadiusSearch(%obj.getPosition(), 10 * %scale, $Typemasks::PlayerObjectType);
				while(isObject(%hit = ContainerSearchNext()))
				{
					if(minigameCanDamage(%obj, %hit) && getMinigameFromObject(%hit).weaponDamage && %hit != %obj)
					{
						%proj = new projectile()
						{
							datablock = IceMProjectile;
							initialPosition = %hit.getPosition();
							initialVelocity = "0 0 5";
							scale = %scale SPC %scale SPC %scale;
							sourceObject = %obj;
							sourceSlot = 0;
							client = %obj.client;
						};
						missionCleanup.add(%proj);
					}
				}
			}
			else if(isObject(%client = %obj.getControllingClient()))
			{
				%client.centerPrint("\c4Not enough MP!<br>\c3" @ %obj.getEnergyLevel() @ "/75", 3);
			}
		}
	}
	function GameConnection::onDeath(%this, %sObj, %sClient, %dType, %dLoc)
	{
		parent::onDeath(%this, %sObj, %sClient, %dType, %dLoc);
		if(!isObject(%this.getControlObject()))
		{
			%this.setControlObject(%this.camera);
			%this.camera.setMode("Corpse", %this.player);
		}
	}
};
activatePackage(MagicIce);