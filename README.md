IMML
====

### Immersive Media Markup Language

_Abstract, lightweight, flexible_

IMML aims to be a lightweight, lean and mean specification that supports spatial presentation of media, without being tied to any particular format or device. 

To achieve this, IMML provides an abstract series of elements as its foundation including:

- Anchor
- Background
- Camera
- Effect
- Light
- Model
- Plugin
- Primitive
- Script
- Shader
- Sound
- Text
- Video
- Widget

> Visit the [wiki](https://github.com/craigomatic/IMML/wiki) for code samples that show how to use the library. 

#### Examples

##### Hello world with an image

	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Primitive Type="Box">
			<MaterialGroup Id="-1">
				<Texture Source="hello-world.jpg" />					
			</MaterialGroup>
		</Primitive>
	</IMML>
	
##### Hello world with video

	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		
		<Primitive Type="Box">
			<MaterialGroup Id="-1">
				<Video Enabled="True" Source="hello-world.webm" />					
			</MaterialGroup>
		</Primitive>
	</IMML>
	
##### Hello world with audio

	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		<Sound Enabled="True" Source="hello-world.ogg" Spatial="True"/>
	</IMML>

	
##### Hello world with text

	<IMML Camera="Camera" xmlns="http://schemas.vastpark.com/2007/imml/">
		<Camera Name="Camera" Position="0,0,-5" />
		<Text Value="Hello World!" Alignment="Centre"/>
	</IMML>
	
#### License

IMML was developed at VastPark under the VastPark Open Specification Promise and has been made available on GitHub under the MIT license.
