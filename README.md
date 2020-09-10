![Alt text](/RepoFiles/GeoXscreenshot_2.0.PNG?raw=true "TitleScreenshot")

<b>GeoXplorer (GeoExplorer)</b>

Using the HoloLens to display 3D photogrammetry and 3D GIS scenes

The Earth and planetary sciences inhabit a three-dimensional environment. As such it makes sense to represent many related data as a 3D structure or model. The current convention is to use 2D screens, which have the advantage of having access to large processing and graphical power, to visualize 3D data. However, there can be visual problems projecting a 3D object on a 2D screen (think map projections) where the observer may misinterpret, or be obscured from, relevant and potentially important structure. This problem is particularly noticeable in the way science research is currently communicated: conferences employ poster sessions and PowerPoint (for example) presentations, and scientists will use 3D graphics within them. It would seem prudent to explore how emerging technologies that allow for an immersive, more realistic 3D visualization system and investigate how these systems can potentially improve communication and teaching of Earth and Planetary science. The goal of this application is to provide accurate representations of geological features on a platform that provides the freedom a geologist would have in the field.
There has been a significant interest in the development of head-mounted displays (HMD). These include both virtual reality (VR: Oculus Rift, Go; HTC Vive; Google Cardboard, etc.) and augmented reality (AR: Microsoft HoloLens, Meta 2, Apple ARKit, etc.) headsets. VR HMD are now becoming reasonably widespread with a number of gaming developers utilizing their immersive capabilities. AR HMD are less developed as they require environment-sensing capabilities in order to have virtual holograms interact with real world objects.

We have identified several preferred features that an emerging platform would need to have to be useful in a teaching environment.
1.	Untethered. Imagine a class of 10 students that can move freely around a room. The same number of cables running from any HMD will provide not only a significant trip-hazard, but will also restrain the student from fully exploring the augmented or virtual environment.
2.	Shared experience. With any teaching environment, it requires the ability for an instructor and students to be able to observe the same thing. Within an HMD environment this requires the accurate sharing of the virtual experience between headsets. Furthermore for a fully VR environment, it would likely be necessary to include avatars for other users to provide reference points for the user. This may also be the case for an AR HMD where the users are at remote locations from each other. In a local AR environment this would be unnecessary as the user can see other users through the transparent screen. This last situation would be the ideal case for a teaching environment as it would be the most natural for both instructor and student, allowing for not just vocal but physical communication.

<b>3D GIS</b>

Exported VRML requires a TIN and the overlain imagery that is exported as JPEG tiles corresponding to a similar tile in the .wrl file. Currently Unity3D does not allow for the direct import of VRML files – as such we parse the VRML through the open source(?) 3D modeling software, Blender. This then allows us to make sure the model is exported correctly from ArcScene (it has been noticed on occasion that the mesh is interpreted joining the mesh endpoints which can create a false mesh face that does not exist, these faces can be deleted within Blender). The imported VRML file in Blender will appear in Blender units (1m = 1 Blender unit) which can lead the model to be much larger than the field of view. Reducing the size of the model may be preferable for editing, but the retention of that scale factor is important if referencing is required at a later development stage. The file can then either be exported as a file format that Unity3D can interpret (e.g. .fbx), or simply as a .blend file.

<b>Agisoft Photoscan Photogrammetry</b>

Many of the models seen withing the application are created using structure-from-motion photogrammetry. This technique has allowed anyone with a camera to recreate a 3D structure without the need for depth sensing instruments such as LiDAR. Microsoft recommends a maximum of 25,000 triangles for any given mesh so that the HoloLens can render at its optimum 60 fps. However, we have opted for detail over performance in many cases and have generated most of the meshes with up to 250,000 triangles that can on the rare occasion cause the HoloLen rendering to be reduceed in frame rate. This is still well below the maximum resolution of what Agisoft Photoscan can generate. The overlain texture is made of an 8K or 2x4K image PNG files that, in some cases, provide more detail than the mesh surface. We find that these parameters provide adequate resolution for the purposes of this app in its current form, with the hope of HoloLens improvements providing capabilities to render more complex models in the future.

<b>App Capabilities</b>

<b>Intro Scene</b>

When the user first loads the application (and select that they need to select microphone capabilities) they are greated with a view of the Earth as it appeared yesterday. The user may rotate the Earth using AirTap-and-drag, and moving closer to the Earth will remove the cloud layer and the user can see an image of the surface of the Earth and the seafloor. A number of labels also will appear with AirTappable text. These will increase over time as more models get added to the remote server. Also in this scene are the Moon and Mars. These can be brought to the center of the scene by clicking on them. After doing so, similar AirTappable icons will appear and you can rotate the planetary body as you could do with the Earth, however currently most of these do not work (only Apollo 17 on the Moon), they will in furture updates! Finally there is also a button next to the app title labelled "Earth Interior". It's best to select this when the Earth is in the center of the scene.

<b>The Earth</b>

By clicking on the Airtappable labels one of two things will happen:
1.	a drop down of more Airtabble labels will appear
2.	the Earth and other planets will disappear and you will see "loading model" pulse above the Gaze Cursor.
When the model is loading you will also see a model title appear, along with a set of voice commands. Below those is a slider that can be AirTapped-and-dragged to scale the loaded model from 1:1 which is the loaded scale to 1:10 which reduces the model size by 10 (this is a slider that is part of the MixedRealityToolkit so while gazing at it the user can say min, center or max to move the scale slider as well.

By now the model should have loaded and it's best to move around and see where to view the model best from. The models are set in orientation and so currently we do not allow for full manipulation of the objects. If the user wants to move the model to a more convenient position there is a functionality to 'teleport' along the model. Gaze at the model and AirTap-and-hold, you should see a floating ring appear, and on release the model will move so that the gazed position moves to beneath your feet.

AirTapping on the model also causes flags to fly out into the model...it's a "development" feature.

Next to the title there is an AirTappable 'Back' button that will take you back to the Intro Scene.

<b>The Moon</b>

Currently only Apollo 17 has an associated model (the landing site DTM and LROC image), however, all other landing sites are labeled.

<b>Mars</b>

The succesful landing sites are labeled (plus the upcoming InSight landing location, fingers crossed). DEMs are in the process of being developed and if you visit WashU we can show you one of the MSL Curiosity scenes used for rover path planning.

<b>Earth Interior</b>

Clicking on this button will replace the Earth with a semi-transparent surface where alpha is controlled by the surface elevation (the higher, the more opaque). This way we can see the continents for reference but also have a clear view of the Earth's interior. You will also notice 2 smaller spheres that denote the Earth's outer (liquid) and inner (solid) cores. A number of buttons will appear below the original button with the following labels:
1.	Plate Boundaries: Downloads the tectonic plate boundaries and represents them as red (rift or strike-slip segments) or white (subduction zone) lines.
2.	Recent Earthquakes: Downloads the last 30 days of M4.5+ earthquake locations from the USGS and plots them in 3D. They are colored by depth and the size of the sphere represents the earthquake magnitude.
	There is an added extra with this button that allows you to search the entire earthquake catalog from 1970 to present for a region of the Earth. To the opposite side of the Earth you will see a series of sliderbars that you can set to define a range of Time, Depth and Magnitude. You will need to also define a region on the Earth's surface by AirTapping it to define one corner of a bounding rectangle and then the AirTapping to define the opposite corner. After all these values are set you can click the Fetch Earthquakes button, the Earth will be replaced by a curved surface of the bounded area you selected and the earthquakes within the user-defined ranges will appear (again colored by depth and scaled by magnitude). The user may click on any of the earthquake spheres to bring up some more information about that particular event. It is best to go back systematically and clearing the menus before making any new selection.
3.	Hotspots: Plots the location of notable intraplate hotspots (e.g. Hawaii) as cones on the Earth's surface.
4.	LLSVP: The Large Low Shear Velocity Provinces are the so-called 'superplumes' in the lower mantle. These regions of low seismic shear velocity are thought to be chemically distinct from the rest of the mantle, potentially spawning mantle upwellings that we see as volcanic hotspots.

[Launch Alpine Fault in GeoXplorer](geoxdl://outcrop?alpinefault)

Coming soon:
Sharing capability
Two-handed manipulation

README IN PROGRESS
