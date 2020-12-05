/*
	The following license supersedes all notices in the source code.

	Copyright (c) 2020 Kurt Dekker/PLBM Games All rights reserved.

	http://www.twitter.com/kurtdekker

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions are
	met:

	Redistributions of source code must retain the above copyright notice,
	this list of conditions and the following disclaimer.

	Redistributions in binary form must reproduce the above copyright
	notice, this list of conditions and the following disclaimer in the
	documentation and/or other materials provided with the distribution.

	Neither the name of the Kurt Dekker/PLBM Games nor the names of its
	contributors may be used to endorse or promote products derived from
	this software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
	IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
	TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
	PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
	HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
	SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
	TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
	PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
	LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
	NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// This is just a handy script to let you bulk-create
// a large number of Datasacks, such as when you are
// replicating a particular namespaced structure.
//
// To use:
//	1. fill out the names in the array below
//	2. update the destination path folder
//	3. enable the [MenuItem...] decorator
//	4. goto your Unity editor menu 'Assets' and run it

public static class DatasackEditorUtils
{
	// 2. update the destination path folder (or just use this one)
	const string DestinationFolderPath = "Assets/Datasack/Resources/Datasacks/";

	// 3. uncomment this [MenuItem...] decorator
//	[MenuItem( "Assets/Create bulk datasacks")]
	static void CreateBulkDatasacks()
	{
		// 1. fill out these names (or load them from a file?)	
		string[] names = new string[] {
			"DatasackName1",
			"DatasackName2",
			"DatasackName3",
		};

		foreach( var nm in names)
		{
			var ds = ScriptableObject.CreateInstance<Datasack>();
			ds.name = nm;

			AssetDatabase.CreateAsset( ds, DestinationFolderPath + nm + ".asset");
		}
	}
}

#endif
