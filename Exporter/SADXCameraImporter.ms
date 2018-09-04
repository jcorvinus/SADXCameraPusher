utility SADXCameraImporter "SADX Camera Importer" width:162 height:225
(
	label lbl1 "Pick a file to open:" pos:[16,44] width:108 height:15
	editText pathText "" pos:[10,67] width:140 height:20
	button importButton "Import" pos:[20,131] width:116 height:23 enabled:true
	button browseButton "Browse" pos:[20,99] width:116 height:23 enabled:true
	
	global filePath = ""
	global pathValid = false
	
	--======== Browsing for input file =========--
	on browseButton pressed do
	(
		filePath = getOpenFileName caption:"Pick a CAMxxxxc.bin file to load" types:"bin File|*.bin"
		if(filePath != undefined) then 
		(
			pathText.Text = filePath
			pathValid = true
		) else
		(
			pathValid = false
		)
	)
	
	--======== Importing selected file =======--
	on importButton pressed do
	(
		if(pathValid == false) then messageBox("Please select a file to load")
		else
		(
			camStream = fopen filePath "rb"
			
			cameraCount = readLong camStream
			
			fseek camStream 64 #seek_set
			
			for c = 1 to cameraCount do
			(				
				camType = 0
				camStartLoc = ftell camStream
				camLocation = [0,0,0]
				camRotation = [0,0,0]
				camScale = [1,1,1]
				camPointA = [0,0,0]
				
				camType = readByte camStream
				readByte camStream -- ignore 2nd flags byte
				readShort camStream -- ignore pan speed/priority
				xrot = readShort camStream
				yrot = readShort camStream
				
				xrot = xrot / 182
				yrot = yrot / 182
				
				camRotation.x = xrot
				camRotation.y = yrot
				
				camLocation.x = readFloat camStream
				camLocation.y = readFloat camStream
				camLocation.z = readFloat camStream
				
				camScale.x = readFloat camStream
				camScale.y = readFloat camStream
				camScale.z = readFloat camStream
				
				readLong camStream -- ignoring another unknown value
				
				camPointA.x = readFloat camStream -- ignoring vector 3 param 1
				camPointA.y = readFloat camStream
				camPointA.z = readFloat camStream
				
				readFloat camStream -- ignoring vector 3 param 2
				readFloat camStream
				readFloat camStream
				
				readFloat camStream -- ignoring float param 3
				
				-- creating representation in-scene
				if(camScale != [0,0,0]) do
				(
					camBox = box length:2.0 Height:2.0 Width:2.0 Mapcoords:true
					camBox.pivot = camBox.center
					camBox.position = camLocation
					camBox.scale = camScale
					camBox.rotation.x_rotation = camRotation.x
					camBox.rotation.y_rotation = camRotation.y
					
					camBox.name = "cam_" +  (formattedprint camStartLoc format:"#x")
					--camBox.name = "cam_" + (formattedprint camType format:"#x") + "_" + (formattedprint camStartLoc format:"#x")
					
					camPointAHelper = point axistripod:true box:true size:10
					camPointAHelper.position = camPointA
					camPointAHelper.name = "point_" + (formattedprint camStartLoc format:"#x")
				)
			)
			
			fclose camStream			
			messageBox "Import Complete!"
		)
	)
)