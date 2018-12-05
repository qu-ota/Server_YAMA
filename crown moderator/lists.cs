function hasItemOnList(%string,%item) {
	for(%i=0;%i<getWordCount(%string);%i++) {
		%word = getWord(%string,%i);
		if(%word $= %item)
			return true;
	}
	return false;
}

function addItemToList(%string,%item) {
	if(!hasItemOnList(%string, %item))
		%string = %string SPC %item;
	return %string;
}

function removeItemFromList(%string,%item) {
	for(%i=0;%i<getWordCount(%string);%i++) {
		%word = getWord(%string,%i);
		if(%word $= %item)
			continue;
		%fString = %fString SPC %word;
	}
	return trim(%fString);
}
