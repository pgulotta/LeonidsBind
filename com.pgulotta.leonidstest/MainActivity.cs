using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

using Com.Plattysoft.Leonids;
using Android.Views.Animations;
using Com.Plattysoft.Leonids.Modifiers;
using Java.Util;

namespace com.pgulotta.leonidstest
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        LinearLayout mLinearLayout;
        TextView mTextView;
        TextView mTitle;
        Spinner mSpinner;
        ParticleSystem mParticleSystem1;
        ParticleSystem mParticleSystem2;
        bool mShouldFollowTouch;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            mLinearLayout = FindViewById < LinearLayout >(Resource.Id.main_layout);
            mTextView = FindViewById<TextView>(Resource.Id.textview);
            mTitle = FindViewById<TextView>(Resource.Id.title);
            mSpinner = FindViewById<Spinner>(Resource.Id.spinner);
            mSpinner.Adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.leonidstest, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            mSpinner.ItemSelected += delegate (object s, AdapterView.ItemSelectedEventArgs e)
            {
                if (e.Position == 0)
                    return;
                if (mParticleSystem1 != null)
                {
                    mParticleSystem1.StopEmitting();
                    mParticleSystem1.Cancel();
                    mParticleSystem1.Dispose();
                    mParticleSystem1 = null;
                }
                if (mParticleSystem2 != null)
                {
                    mParticleSystem2.StopEmitting();
                    mParticleSystem2.Cancel();
                    mParticleSystem2.Dispose();
                    mParticleSystem2 = null;
                }
                mLinearLayout.Invalidate();
                mTitle.Text = mSpinner.SelectedItem.ToString();
                mShouldFollowTouch = false;
                switch (e.Position)
                {
                    case 1:
                        OneShotSimple();
                        break;
                    case 2:
                        OneShotAdvanced();
                        break;
                    case 3:
                        EmiterSimple();
                        break;
                    case 4:
                        EmitBackgroundSimple();
                        break;
                    case 5:
                        EmiterIntermediate();
                        break;
                    case 6:
                        EmitTimeLimited();
                        break;
                    case 7:
                        EmitWithGravity();
                        break;
                    case 8:
                        mShouldFollowTouch = true;
                        break;
                    case 9:
                        AnimatedParticles();
                        break;
                    case 10:
                        Fireworks();
                        break;
                    case 11:
                        Confetti();
                        break;
                    case 12:
                        Dust();
                        break;
                    case 13:
                        Stars();
                        break;
                    default:
                        break;
                }
                mSpinner.SetSelection(0);          
            };
                   
        }

        void EmitTimeLimited()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 1000);
            mParticleSystem1.SetScaleRange(0.7f, 1.3f);
            mParticleSystem1.SetSpeedModuleAndAngleRange(0.07f, 0.16f, 0, 180);
            mParticleSystem1.SetRotationSpeedRange(90, 180);
            mParticleSystem1.SetAcceleration(0.00013f, 90);
            mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem1.Emit(mTextView, 100, 2000);
        }

        void EmitBackgroundSimple()
        {
            mParticleSystem1 = new ParticleSystem(this, 50, Resource.Drawable.star_black, 1000, Resource.Id.main_layout);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.Emit(mTextView, 100);
        }

        void Confetti()
        {
            mParticleSystem1 = new ParticleSystem(this, 80, Resource.Drawable.confeti2, 10000);
            mParticleSystem1.SetSpeedModuleAndAngleRange(0f, 0.1f, 180, 180);
            mParticleSystem1.SetRotationSpeed(144);
            mParticleSystem1.SetAcceleration(0.000017f, 90);  
            mParticleSystem1.Emit(FindViewById(Resource.Id.emiter_top_right), 8);

            mParticleSystem2 = new ParticleSystem(this, 80, Resource.Drawable.confeti3, 10000);
            mParticleSystem2.SetSpeedModuleAndAngleRange(0f, 0.1f, 0, 0);
            mParticleSystem2.SetRotationSpeed(144);
            mParticleSystem2.SetAcceleration(0.000017f, 90);   
            mParticleSystem2.Emit(FindViewById(Resource.Id.emiter_top_left), 8);
        }

        void Dust()
        {
            mParticleSystem1 = new ParticleSystem(this, 4, Resource.Drawable.dust, 3000);
            mParticleSystem1.SetSpeedByComponentsRange(-0.025f, 0.025f, -0.06f, -0.08f);  
            mParticleSystem1.SetAcceleration(0.00001f, 30);
            mParticleSystem1.SetInitialRotationRange(0, 360);
            mParticleSystem1.AddModifier(new AlphaModifier(255, 0, 1000, 3000));
            mParticleSystem1.AddModifier(new ScaleModifier(0.5f, 2f, 0, 1000));
            mParticleSystem1.OneShot(FindViewById(Resource.Id.emiter_bottom), 4);
        }

        void Stars()
        {
            mParticleSystem1 = new ParticleSystem(this, 10, Resource.Drawable.star, 3000); 
            mParticleSystem1.SetSpeedByComponentsRange(-0.1f, 0.1f, -0.1f, 0.02f);
            mParticleSystem1.SetAcceleration(0.000003f, 90);
            mParticleSystem1.SetInitialRotationRange(0, 360);
            mParticleSystem1.SetRotationSpeed(120);
            mParticleSystem1.SetFadeOut(2000);
            mParticleSystem1.AddModifier(new ScaleModifier(0f, 1.5f, 0, 1500));
            mParticleSystem1.OneShot(mTextView, 10);
        }

        void AnimatedParticles()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.animated_confetti, 5000);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.SetRotationSpeedRange(90, 180);
            mParticleSystem1.SetInitialRotationRange(0, 360);
            mParticleSystem1.OneShot(mTextView, 100);
        }

        void EmitWithGravity()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 3000);
            mParticleSystem1.SetAcceleration(0.00013f, 90);
            mParticleSystem1.SetSpeedByComponentsRange(0f, 0f, 0.05f, 0.1f);
            mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem1.EmitWithGravity(mTextView, (int)GravityFlags.Bottom, 30);
        }

        void Fireworks()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 800);
            mParticleSystem1.SetScaleRange(0.7f, 1.3f);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.SetRotationSpeedRange(90, 180);
            mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem1.OneShot(mTextView, 70);

            mParticleSystem2 = new ParticleSystem(this, 100, Resource.Drawable.star_white, 800);
            mParticleSystem2.SetScaleRange(0.7f, 1.3f);
            mParticleSystem2.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem2.SetRotationSpeedRange(90, 180);
            mParticleSystem2.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem2.OneShot(mTextView, 70);
        }

        void OneShotAdvanced()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 800);
            mParticleSystem1.SetScaleRange(0.7f, 1.3f);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.SetAcceleration(0.0001f, 90);
            mParticleSystem1.SetRotationSpeedRange(90, 180);
            mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem1.OneShot(mTextView, 100);
        }

        void EmiterSimple()
        {
            mParticleSystem1 = new ParticleSystem(this, 50, Resource.Drawable.star_black, 1000);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.Emit(mTextView, 100);
        }

        void OneShotSimple()
        {
            mParticleSystem1 = new ParticleSystem(this, 50, Resource.Drawable.star_black, 1000);
            mParticleSystem1.SetSpeedRange(0.1f, 0.25f);
            mParticleSystem1.OneShot(mTextView, 100);
        }

        void EmiterIntermediate()
        {
            mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 1000);
            mParticleSystem1.SetScaleRange(0.7f, 1.3f);
            mParticleSystem1.SetSpeedModuleAndAngleRange(0.07f, 0.16f, 0, 180);
            mParticleSystem1.SetRotationSpeedRange(90, 180);
            mParticleSystem1.SetAcceleration(0.00013f, 90);
            mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
            mParticleSystem1.Emit(mTextView, 100);
        }


        public override bool OnTouchEvent(MotionEvent e)
        {
            if (!mShouldFollowTouch)
                return  base.OnTouchEvent(e);
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    mParticleSystem1 = new ParticleSystem(this, 100, Resource.Drawable.star_black, 800);
                    mParticleSystem1.SetScaleRange(0.7f, 1.3f);
                    mParticleSystem1.SetSpeedRange(0.05f, 0.1f);
                    mParticleSystem1.SetRotationSpeedRange(90, 180);
                    mParticleSystem1.SetFadeOut(200, new AccelerateInterpolator());
                    mParticleSystem1.Emit((int)e.GetX(), (int)e.GetY(), 40);
                    break;
                case MotionEventActions.Move:
                    mParticleSystem1.UpdateEmitPoint((int)e.GetX(), (int)e.GetY());
                    break;
                case MotionEventActions.Up:
                    mParticleSystem1.StopEmitting();
                    break;
            }
            return true;
        }

    }
}
