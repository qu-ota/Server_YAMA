//Slay and Forcekill commands
//Kill command by Tezuni, slay command done by Swollow (edited by Dominoes)

function servercmdKill(%client,%Player)
{
	%player = findClientByName(%player);

	if(%player.bl_id == getNumKeyID())
		return messageClient(%client, '', "\c6You can't kill the host.");

	if(!%client.isModerator && !%client.isAdmin && !%client.isSuperAdmin)
		return messageClient(%client, '', "\c6Sorry, you cannot use that command.");
	
	if(%client.isModerator)
	{
		if(%player.isModerator || %player.isAdmin || %player.isSuperAdmin)
			return messageClient(%client, '', "\c6You cannot force kill server staff.");
	}
	

    if(!isObject(%player))
		return messageClient(%client, '', "\c6Player not found.");

	if(!isObject(%player.player))
		return messageClient(%client,"","\c4"@ %player.name @" \c6is already dead.");
    
    %player.player.kill("suicide");
	messageAll('',"\c3" @ %player.name SPC "was \c0force-killed \c6by\c3" SPC %client.name @ "\c6.");
}

if($Pref::Swol_Slay_AdminLvl $= "")
$Pref::Swol_Slay_AdminLvl = 1;

package swol_slay
{
	function serverCmdSlay(%cl,%n)
	{
		swol_slay(%cl,%n);
	}
	function serverCmdSlayBLID(%cl,%n)
	{
		swol_slay(%cl,%n,"blid");
	}
	function swol_slay(%cl,%input,%mode)
	{
		if((%adminLvl = %cl.swol_getAdminLevel()) < $Pref::Swol_Slay_AdminLvl)
		return;
		if(getSimTime()-%cl.swolLastSlay < 1500)
		return;
		%cl.swolLastSlay = getSimTime();
		
		if(%input $= "")
		%mode = "target";
		if(%mode $= "")
		%mode = "name";
		%vic = 0;
		switch$(%mode)
		{
			case "name":
				%vic = findclientbyname(%input);
			case "blid":
				%vic = findclientbybl_id(%input);
			case "target":
				if(!isObject((%pl = %cl.player)))
				return;
				%eye = vectorScale(%pl.getEyeVector(), 200);
				%pos = %pl.getEyePoint();
				%hit = firstWord(containerRaycast(%pos,vectorAdd(%pos, %eye),$TypeMasks::PlayerObjectType,%pl));
				if(isObject(%hit) && isObject(%hit.client))
				%vic = %hit.client;
		}
		if(!isObject(%vic))
		return;
		if(!isObject((%plv = %vic.player)))
		return;
		
		if(%adminLvl <= %vic.swol_getAdminLevel())
		return;
		%p = new Projectile()
		{
			datablock = Swol_SlayProjectile;
			scale = "1 1 1";
            initialVelocity = "0 0 0";
            initialPosition = %plv.position;
            sourceObject = %plv;
            sourceSlot = 0;
            client = %vic;
		};
		MissionCleanUp.add(%p);
		%plv.kill();
		if(isObject(%plv) && %plv.getState() !$= "dead")
		%plv.kill();
		if(isObject(%plv) && %plv.getState() !$= "dead")
		return messageClient(%cl,'',%vic.getPlayerName() @ " could not be slain.");
		messageAll('',"\c0" @ %cl.getPlayerName() @ " \c6has slain \c0" @ %vic.getPlayerName());
	}
	function gameConnection::swol_getAdminLevel(%this)
	{
		if(%this.isLocalConnection() || ($Server::LAN && $Server::Dedicated) || %this.getBLID() $= getNumKeyID())
		return 4;
		if(%this.isSuperAdmin)
		return 3;
		if(%this.isAdmin)
		return 2;
		if(%this.isModerator || %this.isMod)
		return 1;
		return 0;
	}
};
activatePackage(swol_slay);
datablock ParticleData(Swol_SlayExplosionParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.15;
	constantAcceleration = 0;
	lifetimeMS = 450;
	lifetimeVarianceMS = 200;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 1;
	sizes[1] = 2;
};
datablock ParticleEmitterData(Swol_SlayExplosionEmitter)
{
	ejectionPeriodMS = 150;
	periodVarianceMS = 50;
	ejectionVelocity = 20;
	velocityVariance = 15;
	ejectionOffset = 0.2;
	thetaMin = 115;
	thetaMax = 175;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "Swol_SlayExplosionParticle";
};
datablock ExplosionData(Swol_SlayExplosion)
{
   soundProfile = "";

   lifeTimeMS = 300;

   particleEmitter = Swol_SlayExplosionEmitter;
   particleDensity = 50;
   particleRadius = 0.2;

   emitter[0] = Swol_SlayExplosionEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;

   damageRadius = 0;
   radiusDamage = 0;

   impulseRadius = 0;
   impulseForce = 0;
};
datablock ProjectileData(Swol_SlayProjectile)
{
	lifeTime = 5;
	explodeOnDeath = true;
	explosion = swol_SlayExplosion;
};
