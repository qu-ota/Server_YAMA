//Slay and Forcekill commands
//Kill command by Tezuni, slay command done (basically) by Dominoes

datablock AudioProfile(PlayerSlaySound)
{
	filename = "Add-Ons/Server_YAMA/sounds/LightningZapSound.wav";
	description = AudioDefault3d;
	preload = true;
};
datablock ParticleData(LightningExplosionParticle)
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
datablock ParticleEmitterData(LightningExplosionEmitter)
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
	particles = "LightningTrailParticle";

	uiName = "Player Slay Zap - Explosion";
};

datablock ExplosionData(LightningExplosion)
{
	soundProfile = PlayerSlaySound;

	lifeTimeMS = 250;

	particleEmitter = LightningExplosionEmitter;
	particleDensity = 50;
	particleRadius = 0.5;

	faceViewer = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "30 30 30";
	camShakeAmp = "7 2 7";
	camShakeDuration = 0.6;
	camShakeRadius = 3.65;

	lightStartRadius = 10;
	lightEndRadius = 0;
	lightStartColor = "1 1 0";
	lightEndColor = "1 1 0";

	damageRadius = 0;
	radiusDamage = 0;

	uiName = "Player Slay Zap";
};

//scripts coming soon
