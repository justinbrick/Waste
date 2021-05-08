using Sandbox;

namespace Waste
{
    partial class WasteWeapon : WasteItem, IPlayerControllable
    {
		public virtual AmmoType AmmoType => AmmoType.Pistol;
		public virtual int ClipSize => 16;
		public virtual float ReloadTime => 3.0f;
		public virtual int Bucket => 1;
		public virtual int BucketWeight => 100;

		[NetPredicted]
		public int AmmoClip { get; set; }

		[NetPredicted]
		public TimeSince TimeSinceReload { get; set; }

		[NetPredicted]
		public bool IsReloading { get; set; }

		[NetPredicted]
		public TimeSince TimeSincePrimaryAttack { get; set; }

		[NetPredicted]
		public TimeSince TimeSinceSecondaryAttack { get; set; }

		[NetPredicted]
		public TimeSince TimeSinceDeployed { get; set; }

		public override void Spawn()
		{
			base.Spawn();
		}

		public override void ActiveStart( Entity ent )
		{
			base.ActiveStart( ent );
		}

		public override void ActiveEnd( Entity ent, bool dropped )
		{
			base.ActiveEnd( ent, dropped );
		}

		public override void OnCarryStart( Entity parentEntity )
		{
			base.OnCarryStart( parentEntity );
		}

		public override void OnCarryDrop( Entity parentEntity )
		{
			base.OnCarryDrop( parentEntity );
		}

		public virtual void Reload()
		{
			if ( IsReloading )
				return;

			if ( AmmoClip >= ClipSize )
				return;

			TimeSinceReload = 0;

			// TODO: Inventory Logic

			IsReloading = true;
			Owner.SetAnimParam( "b_reload", true );
			StartReloadEffects();
		}

		public virtual bool CanReload()
		{
			// TODO: Implement
			return false;
		}

		public virtual bool CanPrimaryAttack()
		{
			// TODO: Implement 
			return false;
		}

		public virtual bool CanSecondaryAttack()
		{
			// TODO: Implement 

			return false;
		}

		public virtual void AttackSecondary()
		{
			// TODO: Implement 
			TimeSinceSecondaryAttack = 0;

		}

		public virtual void AttackPrimary()
		{
			TimeSincePrimaryAttack = 0;

			// Play shoot effects
			ShootEffects();
		}

		public virtual void OnReloadFinish()
		{
			IsReloading = false;

			if ( Owner is WastePlayer player )
			{
				// TODO: Inventory Logic
			}
		}

		[ClientRpc]
		public virtual void StartReloadEffects()
		{
			ViewModelEntity?.SetAnimParam( "reload", true );

			// TODO - player third person model reload
		}

		public virtual void OnPlayerControlTick( Player player )
		{
			if ( player != Owner )
				return;

			if ( Owner.Input.Down( InputButton.Reload ) )
			{
				Reload();
			}

			// Could've been deleted by reload
			if ( !Owner.IsValid() )
				return;

			if ( CanPrimaryAttack() )
			{
				TimeSincePrimaryAttack = 0;
				AttackPrimary();
			}


			// Could've been deleted by fire
			if ( !Owner.IsValid() )
				return;

			if ( CanSecondaryAttack() )
			{
				TimeSinceSecondaryAttack = 0;
				AttackSecondary();
			}

			if ( TimeSinceDeployed < 0.6f )
				return;

			if ( IsReloading && TimeSinceReload > ReloadTime )
			{
				OnReloadFinish();
			}
		}

		// Play shoot effects on client only
		[ClientRpc]
		protected virtual void ShootEffects()
		{
			Host.AssertClient();

			Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );

			if ( Owner == Player.Local )
			{
				new Sandbox.ScreenShake.Perlin();
			}

			ViewModelEntity?.SetAnimParam( "fire", true );
			CrosshairPanel?.OnEvent( "fire" );
		}
	}

	// Ammo types
	public enum AmmoType
	{
		Pistol = 1,
		HeavyPistol = 2,
		SMG = 3,
		Rifle = 4,
		Shotgun = 5,
		Sniper = 6
	}
}
