using UnityEngine;
using System.Collections;

public interface IDamageable<T>{
	void Damage (T _damage_amount);
}