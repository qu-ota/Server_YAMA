datablock audioProfile(StoneExplodeSound)
{
	fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/StoneExplode.wav";
	description = AudioDefault3D;
	preload = true;
};
datablock audioProfile(StoneFireSound)
{
	fileName = "Add-Ons/Weapon_ElementalSpells/Sounds/StoneFire.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock ParticleData(StoneAmbientParticle)
{
	dragCoefficient = 0;
	windCoefficient = 0;
	gravityCoefficient = 0.35;
	inheritedVelFactor = 0.25;
	constantAcceleration = 0;
	lifetimeMS = 1200;
	lifetimeVarianceMS = 500;
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = true;
	framesPerSec = 1;
	textureName = "base/data/particles/cloud";

	colors[0] = "0.6 0.35 0 1";
	colors[1] = "0.5 0.25 0 1";
	colors[2] = "0.25 0.125 0 0";
	sizes[0] = 0.25;
	sizes[1] = 0.33;
	sizes[2] = 0.1;
	times[0] = 0;
	times[1] = 0.5;
	times[2] = 1;
};
datablock ParticleEmitterData(StoneAmbientEmitter)
{
	ejectionPeriodMS = 30;
	periodVarianceMS = 5;
	ejectionVelocity = 1;
	velocityVariance = 0;
	ejectionOffset = 0;
	thetaMin = 0;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	particles = StoneAmbientParticle;
	uiName = "Stone - Ambient";
};

datablock DebrisData(stoneShardDebris)
{
   emitters = StoneAmbientEmitter;

	shapeFile = "Add-Ons/Weapon_ElementalSpells/OtherShapes/StoneShard.dts";
	lifetime = 3;
	minSpinSpeed = -200;
	maxSpinSpeed = 200;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 1;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 1.5;
};

datablock ParticleData(StoneExplosionParticle)
{
	dragCoefficient = 1;
	windCoefficient = 0;
	gravityCoefficient = 1;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 1100;
	lifetimeVarianceMS = 300;
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	useInvAlpha = true;
	textureName = "base/data/particles/cloud";
	colors[0] = "0.8 0.65 0.30 1";
	colors[1] = "0.75 0.50 0 1";
	colors[2] = "0.50 0.25 0 0";
	sizes[0] = 0.8;
	sizes[1] = 0.7;
	sizes[2] = 0.6;
	times[0] = 0;
	times[1] = 0.5;
	times[2] = 1;
};
datablock ParticleEmitterData(StoneExplosionEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 25;
	velocityVariance = 0;
	ejectionOffset = 1;
	thetaMin = 0;
	thetaMax = 75;
	phiReferenceVel = 0;
	phiVariance = 360;
	particles = StoneExplosionParticle;
	lifetimeMS = 300;
};

datablock ExplosionData(StoneExplosion)
{
	soundProfile = StoneExplodeSound;

	lifeTimeMS = 200;

	particleEmitter = StoneExplosionEmitter;
	particleDensity = 100;
	particleRadius = 5;

   debris = stoneShardDebris;
   debrisNum = 5;
   debrisNumVariance = 1;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 15;
   debrisThetaMax = 60;
   debrisVelocity = 22;
   debrisVelocityVariance = 2;

	emitter[0] = StoneExplosionEmitter;

	faceViewer = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "1.0 1.0 1.0";
	camShakeDuration = 0.5;
	camShakeRadius = 10.0;

	lightStartRadius = 10;
	lightEndRadius = 1;
	lightStartColor = "1 0.5 0";
	lightEndColor = "1 0.5 0";
};

AddDamageType("StoneSpike", '<bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Stone> %1', '%2 <bitmap:Add-Ons/Weapon_ElementalSpells/Icons/CI_Stone> %1', 1, 1);

datablock ProjectileData(StoneMProjectile)
{
	shapeFile = "base/data/shapes/empty.dts";
   directDamage = 0;
   directDamageType = $DamageType::StoneSpike;
   radiusDamageType = $DamageType::StoneSpike;

   brickExplosionRadius = 5;
   brickExplosionImpact = true;
   brickExplosionForce = 50;
   brickExplosionMaxVolume = 15;
   brickExplosionMaxVolumeFloating = 25;

   impactImpulse = 0;
   verticalImpulse = 0;
   explosion = StoneExplosion;
   particleEmitter = StoneAmbientEmitter;

   muzzleVelocity = 60;
   velInheritFactor = 1;

   armingDelay = 0;
   lifetime = 4000;
   fadeDelay = 3500;
   bounceElasticity = 0.5;
   bounceFriction = 0.20;
   isBallistic = true;
   gravityMod = 1;

   hasLight = true;
   lightRadius = 2;
   lightColor = "1 0.5 0";

   uiName = "Stone Spike";
};

datablock StaticShapeData(StoneSpikeData)
{
	category = "Statics";
	shapeFile = "Add-Ons/Weapon_ElementalSpells/OtherShapes/StoneSpike.dts";
	skinName = 'null';
};

function StoneMProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if(strPos(%col.getClassName(), "Player") != -1)
	{
		%start = %col.getPosition();
		%end = vectorAdd(%start, "0 0 " @ -2 * getWord(%col.getScale(), 2));
		%ray = containerRayCast(%start, %end, $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType);
		if(!isObject(%col = firstWord(%ray)))
		{
			return;
		}
		%pos = posFromRaycast(%ray);
		%normal = normalFromRaycast(%ray);
	}
	if(%col.getType() & $TypeMasks::InteriorObjectType || %col.getType() & $TypeMasks::FxBrickObjectType || %col.getType() & $TypeMasks::TerrainObjectType)
	{
		%scale = %obj.getScale();
		%spike = new TSstatic()
		{
			datablock = StoneSpikeData;
			shapeName = "Add-Ons/Weapon_ElementalSpells/OtherShapes/StoneSpike.dts";
			position = %pos;
			rotation = "0 0 0 0";
			scale = %scale;
		};
		MissionCleanup.add(%spike);
		for(%i = 2; %i <= 24; %i++)
		{
			%spike.schedule(%i * 200, setScale, vectorScale(%scale, 1 / (%i / 2)));
		}
		StoneSpikeData.schedule(10, doDamage, %spike, %obj.sourceObject, %pos, %scale);
		%spike.schedule(5000, delete);
	}
}

function StoneSpikeData::doDamage(%data, %spike, %obj, %pos, %scale)
{
	%scale = getWord(%scale, 2);
	%typemasks = $Typemasks::PlayerObjectType | $Typemasks::VehicleObjectType;
	InitContainerRadiusSearch(%pos, 3 * %scale, %typemasks);
	while(isObject(%hit = ContainerSearchNext()))
	{
		if(minigameCanDamage(%obj, %hit) && getMinigameFromObject(%hit).weaponDamage)
		{
			%hit.damage(%obj, %pos, 10 * %scale, $DamageType::StoneSpike);
			%add = vectorScale(vectorAdd("0 0 10", getRandom(-2, 2) SPC getRandom(-2, 2) SPC getRandom(0, 5)), %scale);
			%hit.setVelocity(vectorAdd(%hit.getVelocity(), %add));
			%hit.lastPusher = %obj.sourceObject;
			%hit.lastPushTime = getSimTime();
		}
	}
	%boxpos = vectorAdd(%pos, "0 0 " @ %scale * 3);
	%boxsize = vectorScale("3 3 7", %scale);
	InitContainerBoxSearch(%boxpos, %boxsize, %typemasks);
	while(isObject(%hit = ContainerSearchNext()))
	{
		if(minigameCanDamage(%obj, %hit) && getMinigameFromObject(%hit).weaponDamage)
		{
			%hit.damage(%obj, %pos, 15 * %scale, $DamageType::StoneSpike);
			%hit.lastPusher = %obj.sourceObject;
			%hit.lastPushTime = getSimTime();
			%add = vectorScale(vectorAdd("0 0 10", getRandom(-5, 5) SPC getRandom(-5, 5) SPC getRandom(0, 5)), %scale);
			%hit.setVelocity(vectorAdd(%hit.getVelocity(), %add));
		}
	}
}

datablock ItemData(StoneMItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "Add-Ons/Weapon_ElementalSpells/ItemShapes/Stone.dts";
	rotate = false;
	mass = 0.5;
	density = 0.7;
	elasticity = 0.6;
	friction = 0.6;
	emap = true;

	uiName = "Magic - Stone";
	iconName = "Add-Ons/Weapon_ElementalSpells/Icons/Icon_Stone";
	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	image = StoneMImage;
	canDrop = true;
};

datablock ShapeBaseImageData(StoneMImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0;
   rotation = "0 0 0";

   correctMuzzleVector = true;

   className = "WeaponImage";

   item = StoneMItem;
   ammo = " ";
	MPused = 75;
   projectile = StoneMProjectile;
   projectileType = Projectile;

   melee = false;
   armReady = true;

   doColorShift = false;
   colorShiftColor = "1 1 1 1";

	NoAbsorb["FireM"] = 1; //Damage from type "FireM" is not reduced
	NoAbsorb["WindM"] = 1; //Damage from type "WindM" is not reduced

	stateName[0]                   = "Activate";
	stateTimeoutValue[0]           = 0.15;
	stateTransitionOnTimeout[0]    = "Ready";
	stateSound[0]                  = weaponSwitchSound;

	stateName[1]                   = "Ready";
	stateTransitionOnTriggerDown[1]= "Fire";
	stateAllowImageChange[1]       = true;
	stateEmitter[1]                = StoneAmbientEmitter;
	stateEmitterTime[1]            = 300;

	stateName[2]                   = "Fire";
	stateTransitionOnTimeout[2]    = "Reload";
	stateTimeoutValue[2]           = 1;
	stateFire[2]                   = true;
	stateAllowImageChange[2]       = false;
	stateSequence[2]               = "Fire";
	stateScript[2]                 = "onFire";
	stateWaitForTimeout[2]         = true;
	stateSound[2]                  = StoneFireSound;
	stateEjectShell[2]             = true;

	stateName[3]                   = "Reload";
	stateTransitionOnTriggerUp[3]  = "Ready";
};

function StoneMImage::onFire(%this, %obj, %slot)
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

package MagicStone
{
	function armor::damage(%this, %obj, %sourceObject, %pos, %amt, %type)
	{
		%img = %obj.getMountedImage(0);
		if(%img == StoneMImage.getID())
		{
			if(!%img.NoAbsorb[$DamageType_Array[%type]])
			{
				%amt*= 0.75;
			}
		}
		parent::damage(%this, %obj, %sourceObject, %pos, %amt, %type);
	}
};
activatePackage(MagicStone);