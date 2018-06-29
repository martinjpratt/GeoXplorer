﻿![Alt text](/RepoFiles/VEscreenshot.PNG?raw=true "TitleScreenshot")

GeoXplorer (GeoExplorer)

Using the HoloLens to display 3D photogrammetry and 3D GIS scenes

Why

The Earth and planetary sciences inhabit a three-dimensional environment. As such it makes sense to represent many related data as a 3D structure or model. The current convention is to use 2D screens, which have the advantage of having access to large processing and graphical power, to visualize 3D data. However, there can be visual problems projecting a 3D object on a 2D screen (think map projections) where the observer may misinterpret, or be obscured from, relevant and potentially important structure. This problem is particularly noticeable in the way science research is currently communicated: conferences employ poster sessions and PowerPoint (for example) presentations, and scientists will use 3D graphics within them. It would seem prudent to explore how emerging technologies that allow for an immersive, more realistic 3D visualization system and investigate how these systems can potentially improve communication and teaching of Earth and Planetary science.
There has been a significant interest in the development of head-mounted displays (HMD). These include both virtual reality (VR: Oculus Rift, Go; HTC Vive; Google Cardboard, etc.) and augmented reality (AR: Microsoft HoloLens, Meta 2, Apple ARKit, etc.) headsets. VR HMD are now becoming reasonably widespread with a number of gaming developers utilizing their immersive capabilities. AR HMD are less developed as they require environment-sensing capabilities in order to have virtual holograms interact with real world objects.

We have identified several preferred features that an emerging platform would need to have to be useful in a teaching environment.
1.	Untethered. Imagine a class of 10 students that can move freely around a room. The same number of cables running from any HMD will provide not only a significant trip-hazard, but will also restrain the student from fully exploring the augmented or virtual environment.
2.	Shared experience. With any teaching environment, it requires the ability for an instructor and students to be able to observe the same thing. Within an HMD environment this requires the accurate sharing of the virtual experience between headsets. Furthermore for a fully VR environment, it would likely be necessary to include avatars for other users to provide reference points for the user. This may also be the case for an AR HMD where the users are at remote locations from each other. In a local AR environment this would be unnecessary as the user can see other users through the transparent screen. This last situation would be the ideal case for a teaching environment as it would be the most natural for both instructor and student, allowing for not just vocal but physical communication.

3D GIS
Exported VRML requires a TIN and the overlain imagery that is exported as JPEG tiles corresponding to a similar tile in the .wrl file. Currently Unity3D does not allow for the direct import of VRML files – as such we parse the VRML through the open source(?) 3D modeling software, Blender. This then allows us to make sure the model is exported correctly from ArcScene (it has been noticed on occasion that the mesh is interpreted joining the mesh endpoints which can create a false mesh face that does not exist, these faces can be deleted within Blender). The imported VRML file in Blender will appear in Blender units (1m = 1 Blender unit) which can lead the model to be much larger than the field of view. Reducing the size of the model may be preferable for editing, but the retention of that scale factor is important if referencing is required at a later development stage. The file can then either be exported as a file format that Unity3D can interpret (e.g. .fbx), or simply as a .blend file.

Agisoft Photoscan Photogrammetry

