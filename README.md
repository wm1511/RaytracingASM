# RaytracingASM
###### University project - raytracer with intersection code written in x86 assembly
## Description
This solution is a final task for an Assembly Languages subject on a university. It consists of three projects:
* UI - main project containing interface created with Windows Forms and most of raytracer code, which is based on [Ray Tracing in One Weekend](https://raytracing.github.io/books/RayTracingInOneWeekend.html).
* LibCS - sphere intersection code written in C#
* LibAsm - sphere intersection code written in x86 assembly (uses mostly SSE2 instruction set)

Program generates an image of scene containing randomly-generated spheres. It also writes a time the rendering took. After rendering an image, it can be saved on local drive.
Properties that can be modified by user:
* Library used for intersection
* Render size
* Thread count
* Max ray reflection count
* Samples per pixel



## Screenshot
![Screenshot](https://github.com/wm1511/RaytracingASM/assets/72276813/cebfc71d-64eb-45e2-b2d2-709c6715c0d1)
