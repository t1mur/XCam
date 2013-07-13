#pragma rtGlobals=1		// Use modern global access method.

Menu "Open File"
	"-"
	"Import WinView/32 SPE File...", Import_SPE_File("")
End
Function /S SPE_Clean(inStr,len)
// This is for cleaning up filenames from IPLAB
	String inStr
	Variable len
	String outStr
	Variable p,start,l
	
	// Remove path from inStr
	p=0
	start=0
	l=strlen(inStr)-1;
	do
		start=p+1
		p=strsearch(inStr,":",start)
	while ((-1!=p) *(p<=l))					// as long as condition is true

	// Use internal IGOR function to hanle most cases
	outStr=CleanupName(inStr[start,l],0)

	// Collapse multiple underscores
	do
		p=0
		p=strsearch(outStr, "__", p)
		if (-1!=p)
			outStr[p,p+1]="_"
		endif
	while (-1!=p)

	// Two loops so underscores eat dashes
	do
		p=0
		p=strsearch(outStr, "-_", p)
		if (-1!=p)
			outStr[p,p+1]="_"
		endif
	while (-1!=p)
	do
		p=0
		p=strsearch(outStr, "_-", p)
		if (-1!=p)
			outStr[p,p+1]="_"
		endif
	while (-1!=p)

	// Trim to desired length
	if (len < strlen(outStr))
		outStr=outStr[0,len-1]
	endif
	return outStr
End

//This function imports SPE files from WinView32 as Images
Function /S Import_SPE_File(strFileName)
	//Name of file to import
	String strFileName
	
	//File handle
	Variable hFile=0
	
	//Type of data in SPE file.  0=float, 1=long int, 2=int, 3=unsigned int
	Variable iPixelType=0
	
	//Igor data type
	Variable iIgorType=0
	
	//Image dimensions in pixels
	Variable cx=0, cy=0
	
	//Number of images(frames) in the file
	Variable iNumFrames=0
	
	//Bytes per pixel
	Variable iBPP = 4
	
	Variable bShow = 0
	
	//Open the file
	if(0==strlen(strFileName))
		Open /D/R/M="Select the .SPE file to import."/T=".SPE" hFile 
		strFileName = S_FileName
		bShow = 1
	endif
	
	Open /R hFile as strFileName
	
	//Clean name of weird characters
	String strCleanName = SPE_Clean(strFileName, 50)
	strCleanName = strCleanName[10,strlen(strCleanName)-5] 
	strCleanName = "Im_" + strCleanName + "_Img"
	
	
	//Move to pos 42, #pixels on x axis
	FSetPos hFile, 42
	FBinRead /B=3 /F=2 /U hFile, cx
	
	//Move to pos 108, specifying pixel data type
	FSetPos hFile, 108
	FBinRead /B=3 /F=2 hFile, iPixelType
	
	//Move to pos 656, #pixels on y axis
	FSetPos hFile, 656
	FBinRead /B=3 /F=2 /U hFile, cy
	
	//Move to pos 1446, #frames
	FSetPos hFile, 1446
	FBinRead /B=3 /F=3 /U hFile, iNumFrames
	
	Close hFile
	
	//Convert iPixelType to iBPP
	if(0==iPixelType)
		iBPP=4
		iIgorType=2
	else
		if(1==iPixelType)
			iBPP=4
			iIgorType=32
		else
			if(2==iPixelType)
				iBPP=2
				iIgorType=16
			else
				if(3==iPixelType)
					iBPP=2
					iIgorType=16+64
				endif
			endif
		endif
	endif
	
	//Print "File Name ", strFileName
	//Print "Pixel Type ", iPixelType
	//Print "Bytes Per Pixel ", iBPP
	//Print "Width :", cx,"   Height :", cy
	
	//Load the data starting at pos 4100
	String strExec ="GBLoadWave /Q/N=" + strCleanName + " /W=" +num2str(iNumFrames) + " /T={"
	strExec = strExec + num2str(iIgorType)+","+ num2str(iIgorType)+"}/B=1 /S=4100\"" + strFileName +"\""
	Execute(strExec)
	
	String strIndexedName
	Variable index =0
	
	DO
		strIndexedName = strCleanName + num2str(index)
		Print strIndexedName
	
		// Process the Image from 1D to 2D
		Redimension /N=(cx,cy) $strIndexedName

	
		IF(bShow)
			// Show on the screen
			Display
			AppendImage $strIndexedName
			ModifyImage $strIndexedName ctab={*,*,Grays,0}
			ModifyGraph width={Plan,1,bottom,left},height={Plan,1,left,bottom}
			ModifyGraph swapxy=1
		ENDIF
		index = index+1
	WHILE(index < iNumFrames)
	
	return strIndexedName
End

//	Macro ImportSPEFilesInDirectory_CR()
//	String strDir
//	
//	Variable path=0
//	
//	IF(strlen(strDir) == 0)
//		NewPath /M="Select Directory" /O /Q path
//	ELSE
//		NewPath /O /Q path strDir
//	ENDIF
//		
//	String strFile=""
//	String strPath=""
//	String strWave=""
//	String strWinName=""
//	String strClut=""
//	
//	Variable index=0
//	Variable nimages=0
//	Variable nWindows=0
//	
//	PathInfo path
//	strPath = S_path
//	Print strPath
//	
//	//	Layout/P=Landscape/C=1
//	Variable /G g_xpos = 0
//	Variable /G g_ypos = 60
//	Variable /G g_nGraphs = 0
//	
//	DO
//		strFile = IndexedFile(path, index, ".spe")
//		
//		IF(strlen(strFile) == 0)
//			BREAK
//		ENDIF
//		
//		index +=1
//		
//		Print strPath+strFile
//		strWave = Import_SPE_File(strPath+strFile)
//	
//		g_xpos = 20
//		IF(StringMatch(strFile, "*abs*") %| StringMatch(strFile, "*norm*"))
//			//Data analysis goes here
//			//access current wave by $strWave[x][y]
//		
//			AnalyzeImage(strWave,nimages)
//
//			//Kill old waves and window
//			strWinName=WinName(0,1)
//			IF (strlen(strWinName))
//	//			DoWindow /K $strWinName
//			ENDIF
//			KillWaves /Z $strWave $strClut
//			
//			nimages+=1
//		ENDIF
//	WHILE(1)//nimages<1)
//	
//	//	PostAnalyzeImage()
//End
