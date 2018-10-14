// ExtraPrimitives.js
//
// Created by: Greg Bassett
// Company: A-Lab Software Limited (http://www.alabsoft.com)
//
// Date: September 2010
// Version: 1.0
//
//
// Copyright © A-Lab Software Limited 2010
//

import System.IO; 

class ExtraPrimitives extends EditorWindow {
 
	@MenuItem ("Window/Extra Primitives")
    static function ShowWindow () {
        EditorWindow.GetWindow (ExtraPrimitives);
    }

	var showRectangular : boolean = true;
	var showSpherical : boolean = false;
	var showTriangular : boolean = false;
	var showCylindrical : boolean = false;
	var showRoom : boolean = false;
	var showRock : boolean = false;
	var showArchway : boolean = false;
	var showSettings : boolean = false;

	private var rectangularScrollPosition : Vector2;
	private var sphericalScrollPosition : Vector2;
	private var triangularScrollPosition : Vector2;
	private var cylindricalScrollPosition : Vector2;
	private var roomScrollPosition : Vector2;
	private var rockScrollPosition : Vector2;
	private var archwayScrollPosition : Vector2;
	
	private var btnImage_cube_001 : Texture = Resources.Load("btnImage_cube_001");
	private var btnImage_cube_002 : Texture = Resources.Load("btnImage_cube_002");
	private var btnImage_cube_003 : Texture = Resources.Load("btnImage_cube_003");
	private var btnImage_cube_004 : Texture = Resources.Load("btnImage_cube_004");
	private var btnImage_cube_005 : Texture = Resources.Load("btnImage_cube_005");
	private var btnImage_cube_006 : Texture = Resources.Load("btnImage_cube_006");
	private var btnImage_cube_007 : Texture = Resources.Load("btnImage_cube_007");
	private var btnImage_cube_008 : Texture = Resources.Load("btnImage_cube_008");
	private var btnImage_cube_009 : Texture = Resources.Load("btnImage_cube_009");
	private var btnImage_cube_010 : Texture = Resources.Load("btnImage_cube_010");
	private var btnImage_cube_011 : Texture = Resources.Load("btnImage_cube_011");
	private var btnImage_cube_012 : Texture = Resources.Load("btnImage_cube_012");
	private var btnImage_cube_013 : Texture = Resources.Load("btnImage_cube_013");
	private var btnImage_cube_014 : Texture = Resources.Load("btnImage_cube_014");
	private var btnImage_cube_015 : Texture = Resources.Load("btnImage_cube_015");
	private var btnImage_cube_016 : Texture = Resources.Load("btnImage_cube_016");
	private var btnImage_cube_017 : Texture = Resources.Load("btnImage_cube_017");
	private var btnImage_cube_018 : Texture = Resources.Load("btnImage_cube_018");
	private var btnImage_cube_019 : Texture = Resources.Load("btnImage_cube_019");
	private var btnImage_cube_020 : Texture = Resources.Load("btnImage_cube_020");
	private var btnImage_cube_021 : Texture = Resources.Load("btnImage_cube_021");
	private var btnImage_cube_022 : Texture = Resources.Load("btnImage_cube_022");
	private var btnImage_cube_023 : Texture = Resources.Load("btnImage_cube_023");

	private var btnImage_sphere_001 : Texture = Resources.Load("btnImage_sphere_001");
	private var btnImage_sphere_002 : Texture = Resources.Load("btnImage_sphere_002");
	private var btnImage_sphere_003 : Texture = Resources.Load("btnImage_sphere_003");
	private var btnImage_sphere_004 : Texture = Resources.Load("btnImage_sphere_004");
	private var btnImage_sphere_005 : Texture = Resources.Load("btnImage_sphere_005");
	private var btnImage_sphere_006 : Texture = Resources.Load("btnImage_sphere_006");
	private var btnImage_sphere_007 : Texture = Resources.Load("btnImage_sphere_007");
	private var btnImage_sphere_008 : Texture = Resources.Load("btnImage_sphere_008");
	private var btnImage_sphere_009 : Texture = Resources.Load("btnImage_sphere_009");

	private var btnImage_triangle_001 : Texture = Resources.Load("btnImage_triangle_001");
	private var btnImage_triangle_002 : Texture = Resources.Load("btnImage_triangle_002");
	private var btnImage_triangle_003 : Texture = Resources.Load("btnImage_triangle_003");
	private var btnImage_triangle_004 : Texture = Resources.Load("btnImage_triangle_004");
	private var btnImage_triangle_005 : Texture = Resources.Load("btnImage_triangle_005");
	private var btnImage_triangle_006 : Texture = Resources.Load("btnImage_triangle_006");
	private var btnImage_triangle_007 : Texture = Resources.Load("btnImage_triangle_007");
	private var btnImage_triangle_008 : Texture = Resources.Load("btnImage_triangle_008");
	private var btnImage_triangle_009 : Texture = Resources.Load("btnImage_triangle_009");
	private var btnImage_triangle_010 : Texture = Resources.Load("btnImage_triangle_010");
	private var btnImage_triangle_011 : Texture = Resources.Load("btnImage_triangle_011");
	private var btnImage_triangle_012 : Texture = Resources.Load("btnImage_triangle_012");
	private var btnImage_triangle_013 : Texture = Resources.Load("btnImage_triangle_013");
	private var btnImage_triangle_014 : Texture = Resources.Load("btnImage_triangle_014");
	private var btnImage_triangle_015 : Texture = Resources.Load("btnImage_triangle_015");

	private var btnImage_cylinder_001 : Texture = Resources.Load("btnImage_cylinder_001");
	private var btnImage_cylinder_002 : Texture = Resources.Load("btnImage_cylinder_002");
	private var btnImage_cylinder_003 : Texture = Resources.Load("btnImage_cylinder_003");
	private var btnImage_cylinder_004 : Texture = Resources.Load("btnImage_cylinder_004");
	private var btnImage_cylinder_005 : Texture = Resources.Load("btnImage_cylinder_005");
	private var btnImage_cylinder_006 : Texture = Resources.Load("btnImage_cylinder_006");
	private var btnImage_cylinder_007 : Texture = Resources.Load("btnImage_cylinder_007");
	private var btnImage_cylinder_008 : Texture = Resources.Load("btnImage_cylinder_008");
	private var btnImage_cylinder_009 : Texture = Resources.Load("btnImage_cylinder_009");
	private var btnImage_cylinder_010 : Texture = Resources.Load("btnImage_cylinder_010");
	private var btnImage_cylinder_011 : Texture = Resources.Load("btnImage_cylinder_011");
	private var btnImage_cylinder_012 : Texture = Resources.Load("btnImage_cylinder_012");
	private var btnImage_cylinder_013 : Texture = Resources.Load("btnImage_cylinder_013");
	private var btnImage_cylinder_014 : Texture = Resources.Load("btnImage_cylinder_014");
	private var btnImage_cylinder_015 : Texture = Resources.Load("btnImage_cylinder_015");
	private var btnImage_cylinder_016 : Texture = Resources.Load("btnImage_cylinder_016");
	private var btnImage_cylinder_017 : Texture = Resources.Load("btnImage_cylinder_017");

	private var btnImage_room_001 : Texture = Resources.Load("btnImage_room_001");
	private var btnImage_room_002 : Texture = Resources.Load("btnImage_room_002");
	private var btnImage_room_003 : Texture = Resources.Load("btnImage_room_003");
	private var btnImage_room_004 : Texture = Resources.Load("btnImage_room_004");
	private var btnImage_room_005 : Texture = Resources.Load("btnImage_room_005");
	private var btnImage_room_006 : Texture = Resources.Load("btnImage_room_006");
	private var btnImage_room_007 : Texture = Resources.Load("btnImage_room_007");
	private var btnImage_room_008 : Texture = Resources.Load("btnImage_room_008");
	private var btnImage_room_009 : Texture = Resources.Load("btnImage_room_009");
	private var btnImage_room_010 : Texture = Resources.Load("btnImage_room_010");
	private var btnImage_room_011 : Texture = Resources.Load("btnImage_room_011");
	private var btnImage_room_012 : Texture = Resources.Load("btnImage_room_012");
	private var btnImage_room_013 : Texture = Resources.Load("btnImage_room_013");

	private var btnImage_rock_001 : Texture = Resources.Load("btnImage_rock_001");
	private var btnImage_rock_002 : Texture = Resources.Load("btnImage_rock_002");
	private var btnImage_rock_003 : Texture = Resources.Load("btnImage_rock_003");
	private var btnImage_rock_004 : Texture = Resources.Load("btnImage_rock_004");
	private var btnImage_rock_005 : Texture = Resources.Load("btnImage_rock_005");
	private var btnImage_rock_006 : Texture = Resources.Load("btnImage_rock_006");
	private var btnImage_rock_007 : Texture = Resources.Load("btnImage_rock_007");
	private var btnImage_rock_008 : Texture = Resources.Load("btnImage_rock_008");
	private var btnImage_rock_009 : Texture = Resources.Load("btnImage_rock_009");
	private var btnImage_rock_010 : Texture = Resources.Load("btnImage_rock_010");
	private var btnImage_rock_011 : Texture = Resources.Load("btnImage_rock_011");
	private var btnImage_rock_012 : Texture = Resources.Load("btnImage_rock_012");
	private var btnImage_rock_013 : Texture = Resources.Load("btnImage_rock_013");
	private var btnImage_rock_014 : Texture = Resources.Load("btnImage_rock_014");
	private var btnImage_rock_015 : Texture = Resources.Load("btnImage_rock_015");

	private var btnImage_archway_001 : Texture = Resources.Load("btnImage_archways_001");
	private var btnImage_archway_002 : Texture = Resources.Load("btnImage_archways_002");
	private var btnImage_archway_003 : Texture = Resources.Load("btnImage_archways_003");
	private var btnImage_archway_004 : Texture = Resources.Load("btnImage_archways_004");
	private var btnImage_archway_005 : Texture = Resources.Load("btnImage_archways_005");
	private var btnImage_archway_006 : Texture = Resources.Load("btnImage_archways_006");
	private var btnImage_archway_007 : Texture = Resources.Load("btnImage_archways_007");
	private var btnImage_archway_008 : Texture = Resources.Load("btnImage_archways_008");
	private var btnImage_archway_009 : Texture = Resources.Load("btnImage_archways_009");
	private var btnImage_archway_010 : Texture = Resources.Load("btnImage_archways_010");
	private var btnImage_archway_011 : Texture = Resources.Load("btnImage_archways_011");
	private var btnImage_archway_012 : Texture = Resources.Load("btnImage_archways_012");


	private var bCreateCollider : boolean = true;

	function OnGUI() {
        showRectangular = EditorGUILayout.Foldout(showRectangular, "Rectangular Primitives"); 
        if (showRectangular)
			{ 
		
			rectangularScrollPosition = EditorGUILayout.BeginScrollView (rectangularScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_001, GUILayout.Width(80),GUILayout.Height(80))) {
				cube001 = Instantiate(Resources.Load("cube_001"), Vector3(0, 0, 0), Quaternion.identity);
				cube001.name = "Cube";
				if(bCreateCollider){
					cube001.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube001.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_002, GUILayout.Width(80),GUILayout.Height(80))) {
				cube002 = Instantiate(Resources.Load("cube_002"), Vector3(0, 0, 0), Quaternion.identity);
				cube002.name = "Cube";
				if(bCreateCollider){
					cube002.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube002.gameObject;
			}			
			if (GUILayout.Button (btnImage_cube_003, GUILayout.Width(80),GUILayout.Height(80))) {
				cube003 = Instantiate(Resources.Load("cube_003"), Vector3(0, 0, 0), Quaternion.identity);
				cube003.name = "Cube";
				if(bCreateCollider){
					cube003.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_004, GUILayout.Width(80),GUILayout.Height(80))) {
				cube004 = Instantiate(Resources.Load("cube_004"), Vector3(0, 0, 0), Quaternion.identity);
				cube004.name = "Cube";
				if(bCreateCollider){
					cube004.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube004.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_005, GUILayout.Width(80),GUILayout.Height(80))) {
				cube005 = Instantiate(Resources.Load("cube_005"), Vector3(0, 0, 0), Quaternion.identity);
				cube005.name = "Cube";
				if(bCreateCollider){
					cube005.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube005.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_006, GUILayout.Width(80),GUILayout.Height(80))) {
				cube006 = Instantiate(Resources.Load("cube_006"), Vector3(0, 0, 0), Quaternion.identity);
				cube006.name = "Cube";
				if(bCreateCollider){
					cube006.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_007, GUILayout.Width(80),GUILayout.Height(80))) {
				cube007 = Instantiate(Resources.Load("cube_007"), Vector3(0, 0, 0), Quaternion.identity);
				cube007.name = "Cube";
				if(bCreateCollider){
					cube007.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube007.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_008, GUILayout.Width(80),GUILayout.Height(80))) {
				cube008 = Instantiate(Resources.Load("cube_008"), Vector3(0, 0, 0), Quaternion.identity);
				cube008.name = "Cube";
				if(bCreateCollider){
					cube008.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube008.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_009, GUILayout.Width(80),GUILayout.Height(80))) {
				cube009 = Instantiate(Resources.Load("cube_009"), Vector3(0, 0, 0), Quaternion.identity);
				cube009.name = "Cube";
				if(bCreateCollider){
					cube009.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube009.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_010, GUILayout.Width(80),GUILayout.Height(80))) {
				cube010 = Instantiate(Resources.Load("cube_010"), Vector3(0, 0, 0), Quaternion.identity);
				cube010.name = "Cube";
				if(bCreateCollider){
					cube010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cube010.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_011, GUILayout.Width(80),GUILayout.Height(80))) {
				cube011 = Instantiate(Resources.Load("cube_011"), Vector3(0, 0, 0), Quaternion.identity);
				cube011.name = "Cube";
				if(bCreateCollider){
					cube011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cube011.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_012, GUILayout.Width(80),GUILayout.Height(80))) {
				cube012 = Instantiate(Resources.Load("cube_012"), Vector3(0, 0, 0), Quaternion.identity);
				cube012.name = "Cube";
				if(bCreateCollider){
					cube012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cube012.gameObject;
			}
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_013, GUILayout.Width(80),GUILayout.Height(80))) {
				cube013 = Instantiate(Resources.Load("cube_013"), Vector3(0, 0, 0), Quaternion.identity);
				cube013.name = "Cube";
				if(bCreateCollider){
					cube013.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube013.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_014, GUILayout.Width(80),GUILayout.Height(80))) {
				cube014 = Instantiate(Resources.Load("cube_014"), Vector3(0, 0, 0), Quaternion.identity);
				cube014.name = "Cube";
				if(bCreateCollider){
					cube014.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube014.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_015, GUILayout.Width(80),GUILayout.Height(80))) {
				cube015 = Instantiate(Resources.Load("cube_015"), Vector3(0, 0, 0), Quaternion.identity);
				cube015.name = "Cube";
				if(bCreateCollider){
					cube015.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube015.gameObject;
			}
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_016, GUILayout.Width(80),GUILayout.Height(80))) {
				cube016 = Instantiate(Resources.Load("cube_016"), Vector3(0, 0, 0), Quaternion.identity);
				cube016.name = "Cube";
				if(bCreateCollider){
					cube016.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube016.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_017, GUILayout.Width(80),GUILayout.Height(80))) {
				cube017 = Instantiate(Resources.Load("cube_017"), Vector3(0, 0, 0), Quaternion.identity);
				cube017.name = "Cube";
				if(bCreateCollider){
					cube017.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube017.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_018, GUILayout.Width(80),GUILayout.Height(80))) {
				cube018 = Instantiate(Resources.Load("cube_018"), Vector3(0, 0, 0), Quaternion.identity);
				cube018.name = "Cube";
				if(bCreateCollider){
					cube018.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube018.gameObject;
			}			
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_019, GUILayout.Width(80),GUILayout.Height(80))) {
				cube019 = Instantiate(Resources.Load("cube_019"), Vector3(0, 0, 0), Quaternion.identity);
				cube019.name = "Cube";
				if(bCreateCollider){
					cube019.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube019.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_020, GUILayout.Width(80),GUILayout.Height(80))) {
				cube020 = Instantiate(Resources.Load("cube_020"), Vector3(0, 0, 0), Quaternion.identity);
				cube020.name = "Cube";
				if(bCreateCollider){
					cube020.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube020.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_021, GUILayout.Width(80),GUILayout.Height(80))) {
				cube021 = Instantiate(Resources.Load("cube_021"), Vector3(0, 0, 0), Quaternion.identity);
				cube021.name = "Cube";
				if(bCreateCollider){
					cube021.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube021.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cube_022, GUILayout.Width(80),GUILayout.Height(80))) {
				cube022 = Instantiate(Resources.Load("cube_022"), Vector3(0, 0, 0), Quaternion.identity);
				cube022.name = "Cube";
				if(bCreateCollider){
					cube022.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube022.gameObject;
			}
			if (GUILayout.Button (btnImage_cube_023, GUILayout.Width(80),GUILayout.Height(80))) {
				cube023 = Instantiate(Resources.Load("cube_023"), Vector3(0, 0, 0), Quaternion.identity);
				cube023.name = "Cube";
				if(bCreateCollider){
					cube023.AddComponent ("BoxCollider");
				}
				Selection.activeGameObject = cube023.gameObject;
			}
			GUILayout.EndHorizontal ();			

			EditorGUILayout.EndScrollView();
		}
		
        showSpherical = EditorGUILayout.Foldout(showSpherical, "Spherical Primitives"); 
        if (showSpherical)
		{

			sphericalScrollPosition = EditorGUILayout.BeginScrollView (sphericalScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_sphere_001, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere001 = Instantiate(Resources.Load("sphere_001"), Vector3(0, 0, 0), Quaternion.identity);
				sphere001.name = "Sphere";
				if(bCreateCollider){
					sphere001.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere001.gameObject;
			}
			if (GUILayout.Button (btnImage_sphere_002, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere002 = Instantiate(Resources.Load("sphere_002"), Vector3(0, 0, 0), Quaternion.identity);
				sphere002.name = "Sphere";
				if(bCreateCollider){
					sphere002.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere002.gameObject;
			}			
			if (GUILayout.Button (btnImage_sphere_003, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere003 = Instantiate(Resources.Load("sphere_003"), Vector3(0, 0, 0), Quaternion.identity);
				sphere003.name = "Sphere";
				if(bCreateCollider){
					sphere003.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_sphere_004, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere004 = Instantiate(Resources.Load("sphere_004"), Vector3(0, 0, 0), Quaternion.identity);
				sphere004.name = "Sphere";
				if(bCreateCollider){
					sphere004.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere004.gameObject;
			}
			if (GUILayout.Button (btnImage_sphere_005, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere005 = Instantiate(Resources.Load("sphere_005"), Vector3(0, 0, 0), Quaternion.identity);
				sphere005.name = "Sphere";
				if(bCreateCollider){
					sphere005.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere005.gameObject;
			}
			if (GUILayout.Button (btnImage_sphere_006, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere006 = Instantiate(Resources.Load("sphere_006"), Vector3(0, 0, 0), Quaternion.identity);
				sphere006.name = "Sphere";
				if(bCreateCollider){
					sphere006.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_sphere_007, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere007 = Instantiate(Resources.Load("sphere_007"), Vector3(0, 0, 0), Quaternion.identity);
				sphere007.name = "Sphere";
				if(bCreateCollider){
					sphere007.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere007.gameObject;
			}
			if (GUILayout.Button (btnImage_sphere_008, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere008 = Instantiate(Resources.Load("sphere_008"), Vector3(0, 0, 0), Quaternion.identity);
				sphere008.name = "Sphere";
				if(bCreateCollider){
					sphere008.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere008.gameObject;
			}
			if (GUILayout.Button (btnImage_sphere_009, GUILayout.Width(80),GUILayout.Height(80))) {
				sphere009 = Instantiate(Resources.Load("sphere_009"), Vector3(0, 0, 0), Quaternion.identity);
				sphere009.name = "Sphere";
				if(bCreateCollider){
					sphere009.AddComponent ("SphereCollider");
				}
				Selection.activeGameObject = sphere009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			EditorGUILayout.EndScrollView();

		}

       showTriangular = EditorGUILayout.Foldout(showTriangular, "Triangular Primitives"); 
        if (showTriangular)
		{
		
			triangularScrollPosition = EditorGUILayout.BeginScrollView (triangularScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_triangle_001, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle001 = Instantiate(Resources.Load("triangle_001"), Vector3(0, 0, 0), Quaternion.identity);
				triangle001.name = "Triangle";
				if(bCreateCollider){
					triangle001.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle001.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_002, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle002 = Instantiate(Resources.Load("triangle_002"), Vector3(0, 0, 0), Quaternion.identity);
				triangle002.name = "Triangle";
				if(bCreateCollider){
					triangle002.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle002.gameObject;
			}			
			if (GUILayout.Button (btnImage_triangle_003, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle003 = Instantiate(Resources.Load("triangle_003"), Vector3(0, 0, 0), Quaternion.identity);
				triangle003.name = "Triangle";
				if(bCreateCollider){
					triangle003.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_triangle_004, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle004 = Instantiate(Resources.Load("triangle_004"), Vector3(0, 0, 0), Quaternion.identity);
				triangle004.name = "Triangle";
				if(bCreateCollider){
					triangle004.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle004.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_005, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle005 = Instantiate(Resources.Load("triangle_005"), Vector3(0, 0, 0), Quaternion.identity);
				triangle005.name = "Triangle";
				if(bCreateCollider){
					triangle005.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle005.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_006, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle006 = Instantiate(Resources.Load("triangle_006"), Vector3(0, 0, 0), Quaternion.identity);
				triangle006.name = "Triangle";
				if(bCreateCollider){
					triangle006.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_triangle_007, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle007 = Instantiate(Resources.Load("triangle_007"), Vector3(0, 0, 0), Quaternion.identity);
				triangle007.name = "Triangle";
				if(bCreateCollider){
					triangle007.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle007.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_008, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle008 = Instantiate(Resources.Load("triangle_008"), Vector3(0, 0, 0), Quaternion.identity);
				triangle008.name = "Triangle";
				if(bCreateCollider){
					triangle008.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle008.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_009, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle009 = Instantiate(Resources.Load("triangle_009"), Vector3(0, 0, 0), Quaternion.identity);
				triangle009.name = "Triangle";
				if(bCreateCollider){
					triangle009.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_triangle_010, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle010 = Instantiate(Resources.Load("triangle_010"), Vector3(0, 0, 0), Quaternion.identity);
				triangle010.name = "Triangle";
				if(bCreateCollider){
					triangle010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle010.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_011, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle011 = Instantiate(Resources.Load("triangle_011"), Vector3(0, 0, 0), Quaternion.identity);
				triangle011.name = "Triangle";
				if(bCreateCollider){
					triangle011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle011.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_012, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle012 = Instantiate(Resources.Load("triangle_012"), Vector3(0, 0, 0), Quaternion.identity);
				triangle012.name = "Triangle";
				if(bCreateCollider){
					triangle012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle012.gameObject;
			}
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_triangle_013, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle013 = Instantiate(Resources.Load("triangle_013"), Vector3(0, 0, 0), Quaternion.identity);
				triangle013.name = "Triangle";
				if(bCreateCollider){
					triangle013.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle013.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_014, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle014 = Instantiate(Resources.Load("triangle_014"), Vector3(0, 0, 0), Quaternion.identity);
				triangle014.name = "Triangle";
				if(bCreateCollider){
					triangle014.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle014.gameObject;
			}
			if (GUILayout.Button (btnImage_triangle_015, GUILayout.Width(80),GUILayout.Height(80))) {
				triangle015 = Instantiate(Resources.Load("triangle_015"), Vector3(0, 0, 0), Quaternion.identity);
				triangle015.name = "Triangle";
				if(bCreateCollider){
					triangle015.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = triangle015.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			EditorGUILayout.EndScrollView();
		
		
		}
		
       showCylindrical = EditorGUILayout.Foldout(showCylindrical, "Cylindrical Primitives"); 
        if (showCylindrical)
		{
		
			cylindricalScrollPosition = EditorGUILayout.BeginScrollView (cylindricalScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_001, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder001 = Instantiate(Resources.Load("cylinder_001"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder001.name = "Cylinder";
				if(bCreateCollider){
					cylinder001.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder001.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_002, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder002 = Instantiate(Resources.Load("cylinder_002"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder002.name = "Cylinder";
				if(bCreateCollider){
					cylinder002.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder002.gameObject;
			}			
			if (GUILayout.Button (btnImage_cylinder_003, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder003 = Instantiate(Resources.Load("cylinder_003"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder003.name = "Cylinder";
				if(bCreateCollider){
					cylinder003.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_004, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder004 = Instantiate(Resources.Load("cylinder_004"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder004.name = "Cylinder";
				if(bCreateCollider){
					cylinder004.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder004.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_005, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder005 = Instantiate(Resources.Load("cylinder_005"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder005.name = "Cylinder";
				if(bCreateCollider){
					cylinder005.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder005.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_006, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder006 = Instantiate(Resources.Load("cylinder_006"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder006.name = "Cylinder";
				if(bCreateCollider){
					cylinder006.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_007, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder007 = Instantiate(Resources.Load("cylinder_007"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder007.name = "Cylinder";
				if(bCreateCollider){
					cylinder007.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder007.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_008, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder008 = Instantiate(Resources.Load("cylinder_008"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder008.name = "Cylinder";
				if(bCreateCollider){
					cylinder008.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder008.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_009, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder009 = Instantiate(Resources.Load("cylinder_009"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder009.name = "Cylinder";
				if(bCreateCollider){
					cylinder009.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_010, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder010 = Instantiate(Resources.Load("cylinder_010"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder010.name = "Cylinder";
				if(bCreateCollider){
					cylinder010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder010.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_011, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder011 = Instantiate(Resources.Load("cylinder_011"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder011.name = "Cylinder";
				if(bCreateCollider){
					cylinder011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder011.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_012, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder012 = Instantiate(Resources.Load("cylinder_012"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder012.name = "Cylinder";
				if(bCreateCollider){
					cylinder012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder012.gameObject;
			}
			GUILayout.EndHorizontal ();			
		
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_013, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder013 = Instantiate(Resources.Load("cylinder_013"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder013.name = "Cylinder";
				if(bCreateCollider){
					cylinder013.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder013.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_014, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder014 = Instantiate(Resources.Load("cylinder_014"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder014.name = "Cylinder";
				if(bCreateCollider){
					cylinder014.AddComponent ("CapsuleCollider");
				}
				Selection.activeGameObject = cylinder014.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_015, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder015 = Instantiate(Resources.Load("cylinder_015"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder015.name = "Cylinder";
				if(bCreateCollider){
					cylinder015.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder015.gameObject;
			}
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_cylinder_016, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder016 = Instantiate(Resources.Load("cylinder_016"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder016.name = "Cylinder";
				if(bCreateCollider){
					cylinder016.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = cylinder016.gameObject;
			}
			if (GUILayout.Button (btnImage_cylinder_017, GUILayout.Width(80),GUILayout.Height(80))) {
				cylinder017 = Instantiate(Resources.Load("cylinder_017"), Vector3(0, 0, 0), Quaternion.identity);
				cylinder017.name = "Cylinder";
				if(bCreateCollider){
					cylinder017.AddComponent ("CapsuleCollider");
				}
				Selection.activeGameObject = cylinder017.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			EditorGUILayout.EndScrollView();
		
		}

        showRoom = EditorGUILayout.Foldout(showRoom, "Room Primitives"); 
        if (showRoom)
		{

			roomScrollPosition = EditorGUILayout.BeginScrollView (roomScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_room_001, GUILayout.Width(80),GUILayout.Height(80))) {
				room001 = Instantiate(Resources.Load("room_001"), Vector3(0, 0, 0), Quaternion.identity);
				room001.name = "Wall";
				if(bCreateCollider){
					room001.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room001.gameObject;
			}
			if (GUILayout.Button (btnImage_room_002, GUILayout.Width(80),GUILayout.Height(80))) {
				room002 = Instantiate(Resources.Load("room_002"), Vector3(0, 0, 0), Quaternion.identity);
				room002.name = "Wall";
				if(bCreateCollider){
					room002.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room002.gameObject;
			}			
			if (GUILayout.Button (btnImage_room_003, GUILayout.Width(80),GUILayout.Height(80))) {
				room003 = Instantiate(Resources.Load("room_003"), Vector3(0, 0, 0), Quaternion.identity);
				room003.name = "Wall";
				if(bCreateCollider){
					room003.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_room_004, GUILayout.Width(80),GUILayout.Height(80))) {
				room004 = Instantiate(Resources.Load("room_004"), Vector3(0, 0, 0), Quaternion.identity);
				room004.name = "Wall";
				if(bCreateCollider){
					room004.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room004.gameObject;
			}
			if (GUILayout.Button (btnImage_room_005, GUILayout.Width(80),GUILayout.Height(80))) {
				room005 = Instantiate(Resources.Load("room_005"), Vector3(0, 0, 0), Quaternion.identity);
				room005.name = "Wall";
				if(bCreateCollider){
					room005.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room005.gameObject;
			}
			if (GUILayout.Button (btnImage_room_006, GUILayout.Width(80),GUILayout.Height(80))) {
				room006 = Instantiate(Resources.Load("room_006"), Vector3(0, 0, 0), Quaternion.identity);
				room006.name = "Wall";
				if(bCreateCollider){
					room006.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_room_007, GUILayout.Width(80),GUILayout.Height(80))) {
				room007 = Instantiate(Resources.Load("room_007"), Vector3(0, 0, 0), Quaternion.identity);
				room007.name = "Wall";
				if(bCreateCollider){
					room007.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room007.gameObject;
			}
			if (GUILayout.Button (btnImage_room_008, GUILayout.Width(80),GUILayout.Height(80))) {
				room008 = Instantiate(Resources.Load("room_008"), Vector3(0, 0, 0), Quaternion.identity);
				room008.name = "Wall";
				if(bCreateCollider){
					room008.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room008.gameObject;
			}
			if (GUILayout.Button (btnImage_room_009, GUILayout.Width(80),GUILayout.Height(80))) {
				room009 = Instantiate(Resources.Load("room_009"), Vector3(0, 0, 0), Quaternion.identity);
				room009.name = "Wall";
				if(bCreateCollider){
					room009.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_room_010, GUILayout.Width(80),GUILayout.Height(80))) {
				room010 = Instantiate(Resources.Load("room_010"), Vector3(0, 0, 0), Quaternion.identity);
				room010.name = "Wall";
				if(bCreateCollider){
					room010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room010.gameObject;
			}
			if (GUILayout.Button (btnImage_room_011, GUILayout.Width(80),GUILayout.Height(80))) {
				room011 = Instantiate(Resources.Load("room_011"), Vector3(0, 0, 0), Quaternion.identity);
				room011.name = "Wall";
				if(bCreateCollider){
					room011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room011.gameObject;
			}
			if (GUILayout.Button (btnImage_room_012, GUILayout.Width(80),GUILayout.Height(80))) {
				room012 = Instantiate(Resources.Load("room_012"), Vector3(0, 0, 0), Quaternion.identity);
				room012.name = "Wall";
				if(bCreateCollider){
					room012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room012.gameObject;
			}
			GUILayout.EndHorizontal ();			
		
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_room_013, GUILayout.Width(80),GUILayout.Height(80))) {
				room013 = Instantiate(Resources.Load("room_013"), Vector3(0, 0, 0), Quaternion.identity);
				room013.name = "Wall";
				if(bCreateCollider){
					room013.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = room013.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			EditorGUILayout.EndScrollView();

		}

        showRock = EditorGUILayout.Foldout(showRock, "Rock Primitives"); 
        if (showRock)
		{

			rockScrollPosition = EditorGUILayout.BeginScrollView (rockScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_rock_001, GUILayout.Width(80),GUILayout.Height(80))) {
				rock001 = Instantiate(Resources.Load("rock_001"), Vector3(0, 0, 0), Quaternion.identity);
				rock001.name = "Rock";
				if(bCreateCollider){
					rock001.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock001.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_002, GUILayout.Width(80),GUILayout.Height(80))) {
				rock002 = Instantiate(Resources.Load("rock_002"), Vector3(0, 0, 0), Quaternion.identity);
				rock002.name = "Rock";
				if(bCreateCollider){
					rock002.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock002.gameObject;
			}			
			if (GUILayout.Button (btnImage_rock_003, GUILayout.Width(80),GUILayout.Height(80))) {
				rock003 = Instantiate(Resources.Load("rock_003"), Vector3(0, 0, 0), Quaternion.identity);
				rock003.name = "Rock";
				if(bCreateCollider){
					rock003.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_rock_004, GUILayout.Width(80),GUILayout.Height(80))) {
				rock004 = Instantiate(Resources.Load("rock_004"), Vector3(0, 0, 0), Quaternion.identity);
				rock004.name = "Rock";
				if(bCreateCollider){
					rock004.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock004.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_005, GUILayout.Width(80),GUILayout.Height(80))) {
				rock005 = Instantiate(Resources.Load("rock_005"), Vector3(0, 0, 0), Quaternion.identity);
				rock005.name = "Rock";
				if(bCreateCollider){
					rock005.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock005.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_006, GUILayout.Width(80),GUILayout.Height(80))) {
				rock006 = Instantiate(Resources.Load("rock_006"), Vector3(0, 0, 0), Quaternion.identity);
				rock006.name = "Rock";
				if(bCreateCollider){
					rock006.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_rock_007, GUILayout.Width(80),GUILayout.Height(80))) {
				rock007 = Instantiate(Resources.Load("rock_007"), Vector3(0, 0, 0), Quaternion.identity);
				rock007.name = "Rock";
				if(bCreateCollider){
					rock007.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock007.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_008, GUILayout.Width(80),GUILayout.Height(80))) {
				rock008 = Instantiate(Resources.Load("rock_008"), Vector3(0, 0, 0), Quaternion.identity);
				rock008.name = "Rock";
				if(bCreateCollider){
					rock008.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock008.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_009, GUILayout.Width(80),GUILayout.Height(80))) {
				rock009 = Instantiate(Resources.Load("rock_009"), Vector3(0, 0, 0), Quaternion.identity);
				rock009.name = "Rock";
				if(bCreateCollider){
					rock009.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_rock_010, GUILayout.Width(80),GUILayout.Height(80))) {
				rock010 = Instantiate(Resources.Load("rock_010"), Vector3(0, 0, 0), Quaternion.identity);
				rock010.name = "Rock";
				if(bCreateCollider){
					rock010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock010.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_011, GUILayout.Width(80),GUILayout.Height(80))) {
				rock011 = Instantiate(Resources.Load("rock_011"), Vector3(0, 0, 0), Quaternion.identity);
				rock011.name = "Rock";
				if(bCreateCollider){
					rock011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock011.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_012, GUILayout.Width(80),GUILayout.Height(80))) {
				rock012 = Instantiate(Resources.Load("rock_012"), Vector3(0, 0, 0), Quaternion.identity);
				rock012.name = "Rock";
				if(bCreateCollider){
					rock012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock012.gameObject;
			}
			GUILayout.EndHorizontal ();			
		
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_rock_013, GUILayout.Width(80),GUILayout.Height(80))) {
				rock013 = Instantiate(Resources.Load("rock_013"), Vector3(0, 0, 0), Quaternion.identity);
				rock013.name = "Rock";
				if(bCreateCollider){
					rock013.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock013.gameObject;
			}	
			if (GUILayout.Button (btnImage_rock_014, GUILayout.Width(80),GUILayout.Height(80))) {
				rock014 = Instantiate(Resources.Load("rock_014"), Vector3(0, 0, 0), Quaternion.identity);
				rock014.name = "Rock";
				if(bCreateCollider){
					rock014.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock014.gameObject;
			}
			if (GUILayout.Button (btnImage_rock_015, GUILayout.Width(80),GUILayout.Height(80))) {
				rock015 = Instantiate(Resources.Load("rock_015"), Vector3(0, 0, 0), Quaternion.identity);
				rock015.name = "Rock";
				if(bCreateCollider){
					rock015.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = rock015.gameObject;
			}			
			GUILayout.EndHorizontal ();	
			
			EditorGUILayout.EndScrollView();

		}

        showArchway = EditorGUILayout.Foldout(showArchway, "Archway Primitives"); 
        if (showArchway)
		{

			archwayScrollPosition = EditorGUILayout.BeginScrollView (archwayScrollPosition); 
        
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_archway_001, GUILayout.Width(80),GUILayout.Height(80))) {
				archway001 = Instantiate(Resources.Load("archway_001"), Vector3(0, 0, 0), Quaternion.identity);
				archway001.name = "Archway";
				if(bCreateCollider){
					archway001.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway001.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_002, GUILayout.Width(80),GUILayout.Height(80))) {
				archway002 = Instantiate(Resources.Load("archway_002"), Vector3(0, 0, 0), Quaternion.identity);
				archway002.name = "Archway";
				if(bCreateCollider){
					archway002.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway002.gameObject;
			}			
			if (GUILayout.Button (btnImage_archway_003, GUILayout.Width(80),GUILayout.Height(80))) {
				archway003 = Instantiate(Resources.Load("archway_003"), Vector3(0, 0, 0), Quaternion.identity);
				archway003.name = "Archway";
				if(bCreateCollider){
					archway003.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway003.gameObject;
			}			
			GUILayout.EndHorizontal ();			

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_archway_004, GUILayout.Width(80),GUILayout.Height(80))) {
				archway004 = Instantiate(Resources.Load("archway_004"), Vector3(0, 0, 0), Quaternion.identity);
				archway004.name = "Archway";
				if(bCreateCollider){
					archway004.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway004.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_005, GUILayout.Width(80),GUILayout.Height(80))) {
				archway005 = Instantiate(Resources.Load("archway_005"), Vector3(0, 0, 0), Quaternion.identity);
				archway005.name = "Archway";
				if(bCreateCollider){
					archway005.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway005.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_006, GUILayout.Width(80),GUILayout.Height(80))) {
				archway006 = Instantiate(Resources.Load("archway_006"), Vector3(0, 0, 0), Quaternion.identity);
				archway006.name = "Archway";
				if(bCreateCollider){
					archway006.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway006.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_archway_007, GUILayout.Width(80),GUILayout.Height(80))) {
				archway007 = Instantiate(Resources.Load("archway_007"), Vector3(0, 0, 0), Quaternion.identity);
				archway007.name = "Archway";
				if(bCreateCollider){
					archway007.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway007.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_008, GUILayout.Width(80),GUILayout.Height(80))) {
				archway008 = Instantiate(Resources.Load("archway_008"), Vector3(0, 0, 0), Quaternion.identity);
				archway008.name = "Archway";
				if(bCreateCollider){
					archway008.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway008.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_009, GUILayout.Width(80),GUILayout.Height(80))) {
				archway009 = Instantiate(Resources.Load("archway_009"), Vector3(0, 0, 0), Quaternion.identity);
				archway009.name = "Archway";
				if(bCreateCollider){
					archway009.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway009.gameObject;
			}
			GUILayout.EndHorizontal ();		

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (btnImage_archway_010, GUILayout.Width(80),GUILayout.Height(80))) {
				archway010 = Instantiate(Resources.Load("archway_010"), Vector3(0, 0, 0), Quaternion.identity);
				archway010.name = "Archway";
				if(bCreateCollider){
					archway010.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway010.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_011, GUILayout.Width(80),GUILayout.Height(80))) {
				archway011 = Instantiate(Resources.Load("archway_011"), Vector3(0, 0, 0), Quaternion.identity);
				archway011.name = "Archway";
				if(bCreateCollider){
					archway011.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway011.gameObject;
			}
			if (GUILayout.Button (btnImage_archway_012, GUILayout.Width(80),GUILayout.Height(80))) {
				archway012 = Instantiate(Resources.Load("archway_012"), Vector3(0, 0, 0), Quaternion.identity);
				archway012.name = "Archway";
				if(bCreateCollider){
					archway012.AddComponent ("MeshCollider");
				}
				Selection.activeGameObject = archway012.gameObject;
			}
			GUILayout.EndHorizontal ();			
			
			EditorGUILayout.EndScrollView();

		}

		showSettings = EditorGUILayout.Foldout(showSettings, "Settings..."); 
        if (showSettings)
		{
			GUILayout.BeginVertical ();
			EditorGUILayout.Space();
			bCreateCollider = GUILayout.Toggle(bCreateCollider, "Create colliders?");				
			EditorGUILayout.Space();
			GUILayout.EndVertical ();
		}

//		GUILayout.Label ("Spherical", EditorStyles.toolbarButton);
//		GUILayout.Label ("Triangular", EditorStyles.toolbarButton);
//		GUILayout.Label ("Cylindrical", EditorStyles.toolbarButton);

	}

}