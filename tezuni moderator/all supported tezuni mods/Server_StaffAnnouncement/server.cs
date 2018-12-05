package StaffAnnouncement 
{
	function StaffAnnouncement(%msg) 
	{
		for (%clientIndex = 0 ; %clientIndex < ClientGroup.getCount() ; %clientIndex++) 
		{
			%client = ClientGroup.getObject(%clientIndex);

			if (%client.isadmin || %client.isModerator) 
			{
				MessageClient(%client, '', "<color:FFFF00>" @ %msg);
			}
		}
	}
};
activatepackage(StaffAnnouncement);
	
