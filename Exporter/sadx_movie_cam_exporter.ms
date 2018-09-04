fn rotationToBams angleIn = 
(
	if(angleIn < 0) do (angleIn = 360+angleIn)
	return (angleIn * 65535) / 360
)

utility SA_MeshExporter "SADX Camera Motion Exporter" width:162 height:420
(
	edittext outpath "" pos:[3,30] width:145 height:20
	label lbl1 "Output Path" pos:[9,15] width:133 height:13
	label lbl2 "Last Frame:" pos:[9,88] width:133 height: 13
	edittext lastFrameText "" pos:[3,105] width:56 height:20
	button exp_exec "Export Cam Path" pos:[8,132] width:139 height:26
	button btn_browse "Browse" pos:[7,55] width:78 height:26
	
	on SA_MeshExporter open do
	(
		frameEnd = animationRange.end.frame as integer
		lastFrameText.text = frameEnd as string
	)
	
	on exp_exec pressed do
	(
		-- check to see if a path was supplied
		if (outpath.text == "") do
		(
			messagebox "Enter a file path."
			exit
		)
		
		-- Make sure the selection isn't null
		if (selection[1] == NULL) do
		(
			messagebox "Please select a valid camera"
			exit
		)
		
		if(classof selection[1] != FreeCamera) do
		(
			messageBox "Error: Selected node is not a FreeCamera."
			exit
		)
		
		if(selection.count != 1) do
		(
			messageBox "Error: Unfortunately, only one camera can be done at a time."
			exit
		)
		
		filename = outpath.text + "/" + selection[1].name + ".campath"
		outputStream = fopen filename "wb"
		
		-- out.pth opened, start writing to it
		camera = selection[1]
		
		highFrame = animationRange.end
		if(lastFrameText.text != "") do
		(
			highFrame = lastFrameText.text as integer
		)
		
		for timeIndex =0 to highFrame do
		(
			sliderTime = timeIndex
			
			writeFloat outputStream camera.position.x
			writeFloat outputStream camera.position.y
			writeFloat outputStream camera.position.z
			
			camRotEuler = quatToEuler2 camera.rotation.controller.value
			
			BAMSRotationX = rotationToBams(camRotEuler.x)	as integer
			BAMSRotationY = rotationToBams(camRotEuler.z)	as integer
			BAMSRotationZ = rotationToBams(camRotEuler.y)	as integer
			
			writeshort outputStream BAMSRotationX
			writeshort outputStream BAMSRotationY
			writeshort outputStream BAMSRotationZ
		)
		
		fclose outputStream
		messagebox "Done Exporting motion!"
	)
	
	on btn_browse pressed do
	(
		filename = getSavePath caption:"Select a folder to export spline data to:" initialDir:(getDir #maxroot)
	
		if (filename != undefined) then
		outpath.text = filename
	)
)
