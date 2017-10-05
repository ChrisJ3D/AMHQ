using System;
using UnityEngine;

namespace NodeEditorFramework {

	[Serializable]
	public class Number {

		private float[] _value;

		public float value {
			get { return _value [0]; }
			set { this._value[0] = value;
				this._value[1] = value;
				this._value[2] = value;
				this._value[3] = value; }
		}

		public float x { 
			get { return _value[0]; }
			set { this._value[0] = value; }
			}
					
		public float y { 
			get { return _value[1]; }
			set { this._value[1] = value; }
			}

		public float z { 
			get { return _value[2]; }	
			set { this._value[2] = value; }
			}

		public float w { 
			get { return _value[3]; }
			set { this._value[3] = value; }
			}

		public Number () {
			_value = new float[4];
		}

		public Number (float val) {
			_value = new float[4];
			_value[0] = val;
			_value[1] = _value[0];
			_value[2] = _value[0];
			_value[3] = _value[0];
		}

		public Number (int val) {
			_value = new float[4];
			_value[0] = Convert.ToSingle(val);
			_value[1] = _value[0];
			_value[2] = _value[0];
			_value[3] = _value[0];
		}

		public Number (bool val) {
			_value = new float[4];
			_value[0] = Convert.ToSingle(val);
			_value[1] = _value[0];
			_value[2] = _value[0];
			_value[3] = _value[0];
		}

		public Number (double val) {
			_value = new float[4];
			_value[0] = Convert.ToSingle(val);
			_value[1] = _value[0];
			_value[2] = _value[0];
			_value[3] = _value[0];
		}

		public Number (Vector2 val) {
			_value = new float[4];
			_value[0] = val.x;
			_value[1] = val.y;
			_value[2] = 0.0f;
			_value[3] = 0.0f;
		}

		public Number (Vector3 val) {
			_value = new float[4];
			_value[0] = val.x;
			_value[1] = val.y;
			_value[2] = val.z;
			_value[3] = 0.0f;
		}

		public Number (Vector4 val) {
			_value = new float[4];
			_value[0] = val.x;
			_value[1] = val.y;
			_value[2] = val.z;
			_value[3] = val.w;
		}

		public int ToInt32() {
			return Convert.ToInt32(_value[0]);
		}

		public bool ToBool() {
			return Convert.ToBoolean(_value[0]);
		}

		public float ToFloat() {
			return _value[0];
		}

		public double ToDouble() {
			return Convert.ToDouble(_value[0]);
		}

		public Vector2 ToVector2() {
			return new Vector2(_value[0], _value[1]);
		}

		public Vector3 ToVector3() {
			return new Vector3(_value[0], _value[1], _value[2]);
		}

		public Vector4 ToVector4() {
			return new Vector4(_value[0], _value[1], _value[2], _value[3]);
		}

		public override string ToString() {
			string label = String.Format("({0}, {1}, {2}, {3})", _value[0], _value[1], _value[2], _value[3]);
			return label;
		}

		public string ToStringShort() {
			string label = _value[0].ToString();
			return label;
		}

		public static Number operator +(Number a, Number b) {
			Number sum = new Number();

			for (int i = 0; i < 4; i++) {
				sum._value[i] = a._value[i] + b._value[i]; 
			}

			return sum;
		}

		public static Number operator -(Number a, Number b) {
			Number difference = new Number();

			for (int i = 0; i < 4; i++) {
				difference._value[i] = a._value[i] - b._value[i]; 
			}

			return difference;
		}

		public static Number operator *(Number a, Number b) {
			Number product = new Number();

			for (int i = 0; i < 4; i++) {
				product._value[i] = a._value[i] * b._value[i]; 
			}

			return product;
		}

		public static Number operator /(Number a, Number b) {
			Number quotient = new Number();

			for (int i = 0; i < 4; i++) {
				quotient._value[i] = a._value[i] / b._value[i]; 
			}

			return quotient;
		}

		public static Number operator %(Number a, Number b) {
			Number remainder = new Number();

			for (int i = 0; i < 4; i++) {
				remainder._value[i] = a._value[i] % b._value[i]; 
			}

			return remainder;
		}

		public static implicit operator bool(Number a) {
			return a.ToBool();
		}

		public static implicit operator int(Number a) {
			return a.ToInt32();
		}

		public static implicit operator float(Number a) {
			return a.ToFloat();
		}

		public static implicit operator double(Number a) {
			return a.ToDouble();
		}

		public static implicit operator string(Number a) {
			return a.ToString();
		}

		public static implicit operator Vector2(Number a) {
			return a.ToVector2();
		}

		public static implicit operator Vector3(Number a) {
			return a.ToVector3();
		}

		public static implicit operator Vector4(Number a) {
			return a.ToVector4();
		}

		public static implicit operator Number(float a) {
			return new Number(a);
		}

		public static implicit operator Number(bool a) {
			return new Number(a);
		}

		public static implicit operator Number(int a) {
			return new Number(a);
		}

		public static implicit operator Number(double a) {
			return new Number(a);
		}

		public static implicit operator Number(Vector2 a) {
			return new Number(a);
		}

		public static implicit operator Number(Vector3 a) {
			return new Number(a);
		}

		public static implicit operator Number(Vector4 a) {
			return new Number(a);
		}
	}
}