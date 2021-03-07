using UniRx.Toolkit;
using UnityEngine;

namespace Kadoy.CrimeNet.Pools {
  public class EntityPool<T> : ObjectPool<T> where T : Component {
    private readonly T prefab;
    private readonly Transform parent;

    public EntityPool(T prefab, Transform parent) {
      this.prefab = prefab;
      this.parent = parent;
    }

    protected override T CreateInstance() {
      var instance = Object.Instantiate(prefab, parent);

      instance.name = prefab.name;

      return instance;
    }
  }
}