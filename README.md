IMML
====

### Immersive Media Markup Language

_Abstract, lightweight, flexible_

IMML aims to be a lightweight, lean and mean specification that supports spatial presentation of media, without being tied to any particular format or device. 

To achieve this, IMML provides an abstract series of elements as its foundation that represent common media types including 3d models, primitive models (box, sphere, etc), sounds, text, video and images.

All elements are defined within an [xsd file](src/Imml/imml.xsd) that can be used to validate markup in any editor that supports xsd files, such as Visual Studio:

![vs-associate-xsd](https://user-images.githubusercontent.com/146438/53659003-300f5d00-3c0f-11e9-904a-4d4c549887ab.gif)


Visit the [wiki](https://github.com/craigomatic/IMML/wiki) for code samples that show how to use the library.

#### Examples

##### Hello world with an image
```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Primitive Type="Box">
			<MaterialGroup Id="-1">
				<Texture Source="hello-world.jpg" />					
			</MaterialGroup>
		</Primitive>
	</IMML>
```	
##### Hello world with video
```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Primitive Type="Box">
			<MaterialGroup Id="-1">
				<Video Enabled="True" Source="hello-world.webm" />					
			</MaterialGroup>
		</Primitive>
	</IMML>
```
##### Hello world with audio
```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		<Sound Enabled="True" Source="hello-world.ogg" Spatial="True"/>
	</IMML>
```
	
##### Hello world with text
```xml
	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		<Text Value="Hello World!" Alignment="Centre"/>
	</IMML>
```

#### License

IMML was developed at VastPark under the VastPark Open Specification Promise and has been made available on GitHub under the MIT license.
