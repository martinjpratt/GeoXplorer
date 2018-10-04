// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Lit/Smooth Shaded Colour By Height"
{
	//Created By Jason Beetham
    Properties
	{
        _HP1("HP1",float) = 1
		_HP2("HP2",float) = 1
		_HP3("HP3",float) = 1
		_HP4("HP4",float) = 1
		_HP5("HP5",float) = 1
		_HP6("HP6",float) = 1
		_HP7("HP7",float) = 1
		_HP8("HP8",float) = 1
		_HP9("HP9",float) = 1
		_HP10("HP10",float) = 1
		_HP11("HP11",float) = 1
		_HP12("HP12",float) = 1
		_HP13("HP13",float) = 1
		_HP14("HP14",float) = 1
		_HP15("HP15",float) = 1
		_HP16("HP16",float) = 1
		_HP17("HP17",float) = 1
		_HP18("HP18",float) = 1
		_HP19("HP19",float) = 1
		_HP20("HP20",float) = 1
		_HP21("HP21",float) = 1
		_HP22("HP22",float) = 1
		_HP23("HP23",float) = 1
		_HP24("HP24",float) = 1
		_HP25("HP25",float) = 1
		_HP26("HP26",float) = 1
		_HP27("HP27",float) = 1
		_HP28("HP28",float) = 1
		_HP29("HP29",float) = 1
		_HP30("HP30",float) = 1
		_HP31("HP31",float) = 1
		_HP32("HP32",float) = 1
		_HP33("HP33",float) = 1
		_HP34("HP34",float) = 1
		_HP35("HP35",float) = 1
		_HP36("HP36",float) = 1
		_HP37("HP37",float) = 1
		_HP38("HP38",float) = 1
		_HP39("HP39",float) = 1
		_HP40("HP40",float) = 1
		_HP41("HP41",float) = 1
		_HP42("HP42",float) = 1
		_HP43("HP43",float) = 1
		_HP44("HP44",float) = 1
		_HP45("HP45",float) = 1
		_HP46("HP46",float) = 1
		_HP47("HP47",float) = 1
		_HP48("HP48",float) = 1
		_HP49("HP49",float) = 1
		_HP50("HP50",float) = 1
		_HP51("HP51",float) = 1
		_HP52("HP52",float) = 1
		_HP53("HP53",float) = 1
		_HP54("HP54",float) = 1
		_HP55("HP55",float) = 1
		_HP56("HP56",float) = 1
		_HP57("HP57",float) = 1
		_HP58("HP58",float) = 1
		_HP59("HP59",float) = 1
		_HP60("HP60",float) = 1
		_HP61("HP61",float) = 1
		_HP62("HP62",float) = 1
		_HP63("HP63",float) = 1
		_HP64("HP64",float) = 1
		_C1("C1",Color) = (0,0,0,1)
		_C2("C2",Color) = (0,0,0,1)
		_C3("C3",Color) = (0,0,0,1)
		_C4("C4",Color) = (0,0,0,1)
		_C5("C5",Color) = (0,0,0,1)
		_C6("C6",Color) = (0,0,0,1)
		_C7("C7",Color) = (0,0,0,1)
		_C8("C8",Color) = (0,0,0,1)
		_C9("C9",Color) = (0,0,0,1)
		_C10("C10",Color) = (0,0,0,1)
		_C11("C11",Color) = (0,0,0,1)
		_C12("C12",Color) = (0,0,0,1)
		_C13("C13",Color) = (0,0,0,1)
		_C14("C14",Color) = (0,0,0,1)
		_C15("C15",Color) = (0,0,0,1)
		_C16("C16",Color) = (0,0,0,1)
		_C17("C17",Color) = (0,0,0,1)
		_C18("C18",Color) = (0,0,0,1)
		_C19("C19",Color) = (0,0,0,1)
		_C20("C20",Color) = (0,0,0,1)
		_C21("C21",Color) = (0,0,0,1)
		_C22("C22",Color) = (0,0,0,1)
		_C23("C23",Color) = (0,0,0,1)
		_C24("C24",Color) = (0,0,0,1)
		_C25("C25",Color) = (0,0,0,1)
		_C26("C26",Color) = (0,0,0,1)
		_C27("C27",Color) = (0,0,0,1)
		_C28("C28",Color) = (0,0,0,1)
		_C29("C29",Color) = (0,0,0,1)
		_C30("C20",Color) = (0,0,0,1)
		_C31("C31",Color) = (0,0,0,1)
		_C32("C32",Color) = (0,0,0,1)
		_C33("C33",Color) = (0,0,0.010416667,1)
		_C34("C34",Color) = (0.028225806,0.028225806,0.04905914,1)
		_C35("C35",Color) = (0.056451613,0.056451613,0.087701613,1)
		_C36("C36",Color) = (0.084677419,0.084677419,0.126344086,1)
		_C37("C37",Color) = (0.112903226,0.112903226,0.164986559,1)
		_C38("C38",Color) = (0.141129032,0.141129032,0.203629032,1)
		_C39("C39",Color) = (0.169354839,0.169354839,0.242271505,1)
		_C40("C40",Color) = (0.197580645,0.197580645,0.280913978,1)
		_C41("C41",Color) = (0.225806452,0.225806452,0.319556452,1)
		_C42("C42",Color) = (0.254032258,0.254032258,0.358198925,1)
		_C43("C43",Color) = (0.282258065,0.282258065,0.396841398,1)
		_C44("C44",Color) = (0.310483871,0.310483871,0.435483871,1)
		_C45("C45",Color) = (0.338709677,0.349126344,0.463709677,1)
		_C46("C46",Color) = (0.366935484,0.387768817,0.491935484,1)
		_C47("C47",Color) = (0.39516129,0.42641129,0.52016129,1)
		_C48("C48",Color) = (0.423387097,0.465053763,0.548387097,1)
		_C49("C49",Color) = (0.451612903,0.503696237,0.576612903,1)
		_C50("C50",Color) = (0.47983871,0.54233871,0.60483871,1)
		_C51("C51",Color) = (0.508064516,0.580981183,0.633064516,1)
		_C52("C52",Color) = (0.536290323,0.619623656,0.661290323,1)
		_C53("C53",Color) = (0.564516129,0.658266129,0.689516129,1)
		_C54("C54",Color) = (0.592741935,0.696908602,0.717741935,1)
		_C55("C55",Color) = (0.620967742,0.735551075,0.745967742,1)
		_C56("C56",Color) = (0.649193548,0.774193548,0.774193548,1)
		_C57("C57",Color) = (0.693044355,0.802419355,0.802419355,1)
		_C58("C58",Color) = (0.736895161,0.830645161,0.830645161,1)
		_C59("C59",Color) = (0.780745968,0.858870968,0.858870968,1)
		_C60("C60",Color) = (0.824596774,0.887096774,0.887096774,1)
		_C61("C61",Color) = (0.868447581,0.915322581,0.915322581,1)
		_C62("C62",Color) = (0.912298387,0.943548387,0.943548387,1)
		_C63("C63",Color) = (0.956149194,0.971774194,0.971774194,1)
		_C64("C64",Color) = (1,1,1,1)
	}
		SubShader
		{
        Lighting on
        Pass
			{
            Cull off
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "AutoLight.cginc"
				#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			   struct appdata
			{
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR;
                float3 normal : NORMAL;
                float4 vertex : POSITION;
            }




;
            struct v2f
			{
                float2 uv : TEXCOORD0;
				fixed4 diff : COLOR;
				float3 normal : NORMAL;
                float4 vertex : POSITION;
            }




;
            float4 _C1;
            float4 _C2;
            float4 _C3;
            float4 _C4;
            float4 _C5;
            float4 _C6;
            float4 _C7;
            float4 _C8;
            float4 _C9;
            float4 _C10;
            float4 _C11;
            float4 _C12;
            float4 _C13;
            float4 _C14;
            float4 _C15;
            float4 _C16;
            float4 _C17;
            float4 _C18;
            float4 _C19;
            float4 _C20;
            float4 _C21;
            float4 _C22;
            float4 _C23;
            float4 _C24;
            float4 _C25;
            float4 _C26;
            float4 _C27;
            float4 _C28;
            float4 _C29;
            float4 _C30;
            float4 _C31;
            float4 _C32;
            float4 _C33;
            float4 _C34;
            float4 _C35;
            float4 _C36;
            float4 _C37;
            float4 _C38;
            float4 _C39;
            float4 _C40;
            float4 _C41;
            float4 _C42;
            float4 _C43;
            float4 _C44;
            float4 _C45;
            float4 _C46;
            float4 _C47;
            float4 _C48;
            float4 _C49;
            float4 _C50;
            float4 _C51;
            float4 _C52;
            float4 _C53;
            float4 _C54;
            float4 _C55;
            float4 _C56;
            float4 _C57;
            float4 _C58;
            float4 _C59;
            float4 _C60;
            float4 _C61;
            float4 _C62;
            float4 _C63;
            float4 _C64;
            float _Scale;
            float _HP1;
            float _HP2;
            float _HP3;
            float _HP4;
            float _HP5;
            float _HP6;
            float _HP7;
            float _HP8;
            float _HP9;
            float _HP10;
            float _HP11;
            float _HP12;
            float _HP13;
            float _HP14;
            float _HP15;
            float _HP16;
            float _HP17;
            float _HP18;
            float _HP19;
            float _HP20;
            float _HP21;
            float _HP22;
            float _HP23;
            float _HP24;
            float _HP25;
            float _HP26;
            float _HP27;
            float _HP28;
            float _HP29;
            float _HP30;
            float _HP31;
            float _HP32;
            float _HP33;
            float _HP34;
            float _HP35;
            float _HP36;
            float _HP37;
            float _HP38;
            float _HP39;
            float _HP40;
            float _HP41;
            float _HP42;
            float _HP43;
            float _HP44;
            float _HP45;
            float _HP46;
            float _HP47;
            float _HP48;
            float _HP49;
            float _HP50;
            float _HP51;
            float _HP52;
            float _HP53;
            float _HP54;
            float _HP55;
            float _HP56;
            float _HP57;
            float _HP58;
            float _HP59;
            float _HP60;
            float _HP61;
            float _HP62;
            float _HP63;
            float _HP64;
			float4 _Position;

            v2f vert(appdata v)
			{
				float4 tempPos = mul(unity_ObjectToWorld, float4(0,0,0,1));
				_Position = mul( unity_WorldToObject,tempPos);
                v2f OUT;
				OUT.normal = v.normal;
                OUT.vertex = UnityObjectToClipPos(v.vertex);
                if (v.vertex.y >= _HP1) {
					OUT.diff = _C1; 
                }


				else if (v.vertex.y >= _HP2 && v.vertex.y < _HP1) {
					OUT.diff = _C2; 

                }


				else if (v.vertex.y >= _HP3 && v.vertex.y < _HP2) {
                   OUT.diff = _C3; 
                }


				else if (v.vertex.y >= _HP4 && v.vertex.y < _HP3){
                     OUT.diff = _C4; 
                }


				else if (v.vertex.y >= _HP5 && v.vertex.y < _HP4){
                     OUT.diff = _C5; 
                }


				else if (v.vertex.y >= _HP6 && v.vertex.y < _HP5){
                     OUT.diff = _C6; 
                }


				else if (v.vertex.y >= _HP7 && v.vertex.y < _HP6){
                     OUT.diff = _C7; 
                }


				else if (v.vertex.y >= _HP8 && v.vertex.y < _HP7){
                     OUT.diff = _C8; 
                }


				else if (v.vertex.y >= _HP9 && v.vertex.y < _HP8){
                     OUT.diff = _C9; 
                }


				else if (v.vertex.y >= _HP10 && v.vertex.y < _HP9){
                     OUT.diff = _C10; 
                }


				else if (v.vertex.y >= _HP11 && v.vertex.y < _HP10){
                     OUT.diff = _C11; 
                }

                else if (v.vertex.y >= _HP12 && v.vertex.y < _HP11) {
					OUT.diff = _C12; 

                }


				else if (v.vertex.y >= _HP13 && v.vertex.y < _HP12) {
                   OUT.diff = _C13; 
                }


				else if (v.vertex.y >= _HP14 && v.vertex.y < _HP13){
                     OUT.diff = _C14; 
                }


				else if (v.vertex.y >= _HP15 && v.vertex.y < _HP14){
                     OUT.diff = _C15; 
                }


				else if (v.vertex.y >= _HP16 && v.vertex.y < _HP15){
                     OUT.diff = _C16; 
                }


				else if (v.vertex.y >= _HP17 && v.vertex.y < _HP16){
                     OUT.diff = _C17; 
                }


				else if (v.vertex.y >= _HP18 && v.vertex.y < _HP17){
                     OUT.diff = _C18; 
                }


				else if (v.vertex.y >= _HP19 && v.vertex.y < _HP18){
                     OUT.diff = _C19; 
                }

                else if (v.vertex.y >= _HP20 && v.vertex.y < _HP19){
                     OUT.diff = _C20; 
                }


                else if (v.vertex.y >= _HP21 && v.vertex.y < _HP20){
                     OUT.diff = _C21; 
                }

                else if (v.vertex.y >= _HP22 && v.vertex.y < _HP21) {
					OUT.diff = _C22; 

                }


				else if (v.vertex.y >= _HP23 && v.vertex.y < _HP22) {
                   OUT.diff = _C23; 
                }


				else if (v.vertex.y >= _HP24 && v.vertex.y < _HP23){
                     OUT.diff = _C24; 
                }


				else if (v.vertex.y >= _HP25 && v.vertex.y < _HP24){
                     OUT.diff = _C25; 
                }


				else if (v.vertex.y >= _HP26 && v.vertex.y < _HP25){
                     OUT.diff = _C26; 
                }


				else if (v.vertex.y >= _HP27 && v.vertex.y < _HP26){
                     OUT.diff = _C27; 
                }


				else if (v.vertex.y >= _HP28 && v.vertex.y < _HP27){
                     OUT.diff = _C28; 
                }


				else if (v.vertex.y >= _HP29 && v.vertex.y < _HP28){
                     OUT.diff = _C29; 
                }


				else if (v.vertex.y >= _HP30 && v.vertex.y < _HP29){
                     OUT.diff = _C30; 
                }


				else if (v.vertex.y >= _HP31 && v.vertex.y < _HP30){
                     OUT.diff = _C31; 
                }        

                else if (v.vertex.y >= _HP32 && v.vertex.y < _HP31) {
					OUT.diff = _C32; 

                }


				else if (v.vertex.y >= _HP33 && v.vertex.y < _HP32) {
                   OUT.diff = _C33; 
                }


				else if (v.vertex.y >= _HP34 && v.vertex.y < _HP33){
                     OUT.diff = _C34; 
                }


				else if (v.vertex.y >= _HP35 && v.vertex.y < _HP34){
                     OUT.diff = _C35; 
                }


				else if (v.vertex.y >= _HP36 && v.vertex.y < _HP35){
                     OUT.diff = _C36; 
                }


				else if (v.vertex.y >= _HP37 && v.vertex.y < _HP36){
                     OUT.diff = _C37; 
                }


				else if (v.vertex.y >= _HP38 && v.vertex.y < _HP37){
                     OUT.diff = _C38; 
                }


				else if (v.vertex.y >= _HP39 && v.vertex.y < _HP38){
                     OUT.diff = _C39; 
                }


				else if (v.vertex.y >= _HP40 && v.vertex.y < _HP39){
                     OUT.diff = _C40; 
                }


				else if (v.vertex.y >= _HP41 && v.vertex.y < _HP40){
                     OUT.diff = _C41; 
                }

                else if (v.vertex.y >= _HP42 && v.vertex.y < _HP41) {
					OUT.diff = _C42; 

                }


				else if (v.vertex.y >= _HP43 && v.vertex.y < _HP42) {
                   OUT.diff = _C43; 
                }


				else if (v.vertex.y >= _HP44 && v.vertex.y < _HP43){
                     OUT.diff = _C44; 
                }


				else if (v.vertex.y >= _HP45 && v.vertex.y < _HP44){
                     OUT.diff = _C45; 
                }


				else if (v.vertex.y >= _HP46 && v.vertex.y < _HP45){
                     OUT.diff = _C46; 
                }


				else if (v.vertex.y >= _HP47 && v.vertex.y < _HP46){
                     OUT.diff = _C47; 
                }


				else if (v.vertex.y >= _HP48 && v.vertex.y < _HP47){
                     OUT.diff = _C48; 
                }


				else if (v.vertex.y >= _HP49 && v.vertex.y < _HP48){
                     OUT.diff = _C49; 
                }


				else if (v.vertex.y >= _HP50 && v.vertex.y < _HP49){
                     OUT.diff = _C50; 
                }


				else if (v.vertex.y >= _HP51 && v.vertex.y < _HP50){
                     OUT.diff = _C51; 
                }

                else if (v.vertex.y >= _HP52 && v.vertex.y < _HP51) {
					OUT.diff = _C52; 

                }


				else if (v.vertex.y >= _HP53 && v.vertex.y < _HP52) {
                   OUT.diff = _C53; 
                }


				else if (v.vertex.y >= _HP54 && v.vertex.y < _HP53){
                     OUT.diff = _C54; 
                }


				else if (v.vertex.y >= _HP55 && v.vertex.y < _HP54){
                     OUT.diff = _C55; 
                }


				else if (v.vertex.y >= _HP56 && v.vertex.y < _HP55){
                     OUT.diff = _C56; 
                }


				else if (v.vertex.y >= _HP57 && v.vertex.y < _HP56){
                     OUT.diff = _C57; 
                }


				else if (v.vertex.y >= _HP58 && v.vertex.y < _HP57){
                     OUT.diff = _C58; 
                }


				else if (v.vertex.y >= _HP59 && v.vertex.y < _HP58){
                     OUT.diff = _C59; 
                }


				else if (v.vertex.y >= _HP60 && v.vertex.y < _HP59){
                     OUT.diff = _C60; 
                }


				else if (v.vertex.y >= _HP61 && v.vertex.y < _HP60){
                     OUT.diff = _C61; 
                }

                else if (v.vertex.y >= _HP62 && v.vertex.y < _HP61){
                     OUT.diff = _C62; 
                }

                else if (v.vertex.y >= _HP63 && v.vertex.y < _HP62){
                     OUT.diff = _C63; 
                }

				else {
                    OUT.diff = _C64;
                }

                return OUT;
            }





				fixed4 frag(v2f i) : SV_Target
				{
                float4 col = i.diff;
                half3 lightColor = ShadeVertexLights(i.vertex, i.normal);
                col.rgb *= lightColor;
                return col;
            }

ENDCG
}
}
}