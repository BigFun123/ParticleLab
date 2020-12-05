using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLab.src
{
    public class BulletInit
    {
        DiscreteDynamicsWorld world;
        Dictionary<string, WeakReference> disposeQueue = new Dictionary<string, WeakReference>();
        List<RigidBody> Photons =  new List<RigidBody>();
        List<RigidBody> StaticPhotons = new List<RigidBody>();
        List<RigidBody> Particles = new List<RigidBody>();
        List<RigidBody> Walls = new List<RigidBody>();
        Pen ParticlePen = new Pen(Color.Red);

        Brush PhotonBrush = new SolidBrush(Color.Blue);
        double scale = 10;
        double particleSize = 0.15;
        double photonSize = 4;
        BackgroundWorker _worker;

        public BulletInit()
        {          
            SetupWorker();
        }
        void SetupWorker()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += _worker_DoWork;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.RunWorkerAsync();
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        public void Dispose()
        {
            _worker.CancelAsync();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var conf = new DefaultCollisionConfiguration();
            var dispatcher = new CollisionDispatcher(conf);
            var broadphase = new DbvtBroadphase();
            var Solver = new SequentialImpulseConstraintSolver();
            //var Solver = new MultiBodyConstraintSolver();

            world = new DiscreteDynamicsWorld(dispatcher, broadphase, Solver, conf);
            world.Gravity = new Vector3(0, 0, 0);
            world.SolverInfo.NumIterations = 120;
            world.SolverInfo.SolverMode = SolverModes.RandomizeOrder;
            world.DispatchInfo.UseContinuous = true;
            CreateParticles();
            CreateWalls();
            SetupDoubleSlit();
            
            while ( _worker.CancellationPending == false)
            {
                world.StepSimulation(1.0f / 60.0f);

                for ( int i=0; i< Photons.Count; i++)
                {
                    RigidBody r = Photons[i];
                    r.ApplyCentralImpulse(new Vector3(5, 0, 0));
                }
            }
        }

        public void AddPhoton()
        {
            SphereShape ss = new SphereShape(photonSize);
            RigidBody Photon = CreateBody(1, ss, new Vector3(10, 10, 0));
            Photon.ApplyCentralImpulse(new Vector3(15, 5, 0));
            Photons.Add(Photon);
        }

        public void AddGravityExperiment()
        {
            SphereShape ss = new SphereShape(photonSize);
            RigidBody Photon = CreateBody(1, ss, new Vector3(20, 15, 0));
            //Photon.ApplyCentralImpulse(new Vector3(15, 5, 0));
            StaticPhotons.Add(Photon);
            
            RigidBody Photon2 = CreateBody(1, ss, new Vector3(30, 15, 0));
            //Photon2.ApplyCentralImpulse(new Vector3(15, 5, 0));
            StaticPhotons.Add(Photon2);
        }

        void CreateParticles()
        {
            //ZeroBody zb = new ZeroShape();

            BoxShape brick = new BoxShape(0.1, 0.1, 0.1);
            

            RigidBody fix = CreateBody(0, brick, new Vector3(0, 0, 0));
            fix.SetSleepingThresholds(0, 0);
            fix.SetAnisotropicFriction(Vector3.Zero, AnisotropicFrictionFlags.FrictionDisabled);
            fix.Friction = 0;
            fix.SetDamping(0, 0);

            Random rand = new Random();
            SphereShape ss = new SphereShape(particleSize);
            for ( int xx=0; xx< 115; xx++)
                for (int yy = 0; yy < 90; yy++)
                {                
                RigidBody r = CreateBody(1, ss, 
                    new Vector3(5 + xx * 0.51, 5 + yy * 0.51, 0));
                r.Friction = 0.0;
                r.Restitution = 1;
                r.RollingFriction = 0;
                r.SetDamping(0.01, 0.1);
                Particles.Add(r);
                r.SetSleepingThresholds(0, 0);
                r.SetAnisotropicFriction(Vector3.Zero, AnisotropicFrictionFlags.FrictionDisabled);
                var c = new Generic6DofConstraint(fix, r, Matrix.Translation(1, 0, 0), Matrix.Translation(1, 0, 0), true);
                //c.BreakingImpulseThreshold = 999999999999999;
                c.SetLimit(0, 0, 150);
                c.SetLimit(1, 0, 100);
                c.SetLimit(2, 0, 0);
                c.SetLimit(3, 0, 0);
                c.SetLimit(4, 0, 0);
                c.SetLimit(5, 0, 0);
                //r.ApplyCentralImpulse(new Vector3(10, 10, 0));
                //r.ApplyCentralForce(new Vector3(5, 5, 0));
                        
                world.AddConstraint(c);
            }
            Console.WriteLine("Created");
        }

        void CreateWalls()
        {
            BoxShape brick = new BoxShape(130, 2, 15);

            CreateFixed(brick, new Vector3(50, 0, 0));
            CreateFixed(brick, new Vector3(50, 51, 0));

            BoxShape wall = new BoxShape(2, 130, 15);
            CreateFixed(wall, new Vector3(0, 50, 0));
            CreateFixed(wall, new Vector3(70, 50, 0));
        }

        void SetupDoubleSlit()
        {
            BoxShape wall = new BoxShape(10, 1, 1);
            CreateFixed(wall, new Vector3(0, 30, 0));
            CreateFixed(wall, new Vector3(25, 30, 0));
            CreateFixed(wall, new Vector3(50, 30, 0));
        }

        RigidBody CreateFixed(CollisionShape shape, Vector3 pos)
        {
            RigidBody r4 = CreateBody(0, shape, pos);
            r4.Friction = 0;
            r4.RollingFriction = 0;
            r4.Restitution = 0.9;
            r4.SetAnisotropicFriction(Vector3.Zero, AnisotropicFrictionFlags.FrictionDisabled);
            r4.SetDamping(0, 0);
            Walls.Add(r4);
            world.AddRigidBody(r4);
            return r4;
        }

        public void Pulse()
        {
           
        }

        public void PaintTo(Graphics g)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                RigidBody r = Particles[i];
                g.DrawEllipse(ParticlePen, (float)(r.WorldTransform.Origin.X * scale), (float)(r.WorldTransform.Origin.Y * scale),
                    (float)(particleSize * scale * 2), (float)(particleSize * scale * 2));
            }
            for ( int i=0; i< Photons.Count; i++ )
            {
                RigidBody p = Photons[i];
                g.FillEllipse(PhotonBrush, (float)(p.WorldTransform.Origin.X * scale - photonSize*scale), 
                    (float)(p.WorldTransform.Origin.Y * scale - photonSize*scale),
                    (float)(photonSize * scale * 2), 
                    (float)(photonSize * scale * 2));
            }

            for (int i = 0; i < StaticPhotons.Count; i++)
            {
                RigidBody p = StaticPhotons[i];
                g.FillEllipse(PhotonBrush, (float)(p.WorldTransform.Origin.X * scale - photonSize * scale),
                    (float)(p.WorldTransform.Origin.Y * scale - photonSize * scale),
                    (float)(photonSize * scale * 2),
                    (float)(photonSize * scale * 2));
            }

            for (int i = 0; i < Walls.Count; i++)
            {
                RigidBody r = Walls[i];
                BoxShape s = (BoxShape)r.CollisionShape;
                float fscale = (float)scale;
                g.DrawRectangle(ParticlePen,
                    (float)(r.WorldTransform.Origin.X - s.HalfExtentsWithoutMargin.X) * fscale,
                    (float)(r.WorldTransform.Origin.Y - s.HalfExtentsWithoutMargin.Y) * fscale,
                    (float)(s.HalfExtentsWithoutMargin.X * 2) * fscale,
                    (float)(s.HalfExtentsWithoutMargin.Y * 2) * fscale);
            }
        }

        RigidBody CreateBody(float mass, CollisionShape shape, Vector3 offset)
        {
            var constInfo = new RigidBodyConstructionInfo(mass, new DefaultMotionState(), shape, Vector3.Zero);
            if (mass != 0.0f)
            {
                constInfo.LocalInertia = constInfo.CollisionShape.CalculateLocalInertia(mass);
            }
            var collisionObject = new RigidBody(constInfo);
            collisionObject.Translate(offset);
            world.AddRigidBody(collisionObject);

            AddToDisposeQueue(constInfo);
            AddToDisposeQueue(constInfo.MotionState);
            AddToDisposeQueue(collisionObject);
            AddToDisposeQueue(shape);

            //collisionObject.OnDisposing += onDisposing;
            //collisionObject.OnDisposed += onDisposed;

            return collisionObject;
        }

        
        protected void AddToDisposeQueue(object obj, string name = "")
        {
            var r = new WeakReference(obj);
            if (name.Length == 0)
            {
                name = obj.GetType().Name;
            }
            if (disposeQueue.ContainsKey(name))
            {
                int i = 2;
                var name2 = name + i.ToString();
                while (disposeQueue.ContainsKey(name2))
                {
                    i++;
                    name2 = name + i.ToString();
                }
                name = name2;
            }
            disposeQueue.Add(name, r);
        }
    }
}
