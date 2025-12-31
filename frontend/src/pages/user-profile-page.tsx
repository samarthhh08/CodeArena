import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FaCheckCircle } from "react-icons/fa";
import Loading from "@/components/loading";

type Submission = {
  title: string;
  status: string;
  language: string;
};

type UserProfile = {
  username: string;
  email: string;
  about: string;
  latestSubmissions: Submission[];
};

const UserProfilePage = () => {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUserProfile = async () => {
      try {
        const res = await axios.get("http://localhost:5046/api/users/profile", {
          withCredentials: true,
        });
        setProfile(res.data.data);
      } catch (error) {
        if (error instanceof AxiosError) {
          console.error(error.response?.data);
        }
      } finally {
        setLoading(false);
      }
    };

    fetchUserProfile();
  }, []);

  if (loading) {
    return <Loading></Loading>;
  }

  if (!profile) {
    return <div className="p-6">Failed to load profile</div>;
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 to-blue-200 p-6">
      <div className="max-w-7xl mx-auto grid grid-cols-12 gap-6">
        {/* LEFT SIDEBAR */}
        <div className="col-span-3 bg-white rounded-xl shadow p-6">
          <div className="flex flex-col items-center text-center">
            <div className="w-24 h-24 rounded-full bg-blue-100 flex items-center justify-center text-3xl font-bold text-blue-600">
              {profile.username[0].toUpperCase()}
            </div>

            <h2 className="mt-4 text-lg font-bold">{profile.username}</h2>
            <p className="text-sm text-gray-500">{profile.email}</p>

            {/* <button className="mt-4 px-4 py-2 bg-blue-600 text-white rounded-md text-sm">
              Edit Profile
            </button> */}
          </div>

          {/* <div className="mt-8 space-y-3 text-sm">
            <div className="font-medium text-blue-600">Profile</div>
            <div className="text-gray-500">Submissions</div>
            <div className="text-gray-500">Settings</div>
            <div className="text-gray-500">Log Out</div>
          </div> */}
        </div>

        {/* MAIN CONTENT */}
        <div className="col-span-9 space-y-6">
          {/* PROFILE HEADER */}
          {/* <div className="bg-white rounded-xl shadow p-6 flex justify-between">
            <h1 className="text-xl font-bold">User Profile</h1>
            <button className="px-4 py-2 bg-blue-600 text-white rounded-md text-sm">
              Edit
            </button>
          </div> */}

          {/* STATS */}
          <div className="grid grid-cols-4 gap-4">
            <StatCard label="Problems Solved" value="—" />
            <StatCard label="Hard" value="—" />
            <StatCard label="Medium" value="—" />
            <StatCard label="Easy" value="—" />
          </div>

          {/* ABOUT + LANGUAGES */}
          <div className="grid grid-cols-3 gap-6">
            <div className="col-span-2 bg-white rounded-xl shadow p-6">
              <h2 className="font-semibold mb-2">About Me</h2>
              {profile.about ? (
                <p className="text-sm text-gray-700">{profile.about}</p>
              ) : (
                <p className="text-sm text-gray-400">No bio added yet.</p>
              )}
            </div>

            <div className="bg-white rounded-xl shadow p-6">
              <h2 className="font-semibold mb-2">Languages</h2>
              <div className="flex flex-wrap gap-2 text-sm">
                <span className="px-2 py-1 bg-gray-100 rounded">C++</span>
                <span className="px-2 py-1 bg-gray-100 rounded">Java</span>
                <span className="px-2 py-1 bg-gray-100 rounded">Python</span>
              </div>
            </div>
          </div>

          {/* RECENT SUBMISSIONS */}
          <div className="bg-white rounded-xl shadow p-6">
            <h2 className="font-semibold mb-4">Recent Submissions</h2>

            {profile.latestSubmissions.length === 0 ? (
              <p className="text-sm text-gray-400">No submissions yet.</p>
            ) : (
              <div className="space-y-3">
                {profile.latestSubmissions.map((s, idx) => (
                  <Link
                    key={idx}
                    to={`/problems/${encodeURIComponent(s.title)}`}
                    className="flex justify-between items-center p-3 bg-gray-50 rounded hover:bg-gray-100"
                  >
                    <div className="flex items-center gap-2">
                      <FaCheckCircle className="text-green-500" />
                      <span className="font-medium">{s.title}</span>
                    </div>

                    <div className="text-xs text-gray-500">
                      {s.language.toUpperCase()}
                    </div>
                  </Link>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserProfilePage;

/* ------------------ SMALL COMPONENT ------------------ */

const StatCard = ({ label, value }: { label: string; value: string }) => (
  <div className="bg-white rounded-xl shadow p-4 text-center">
    <div className="text-lg font-bold">{value}</div>
    <div className="text-xs text-gray-500">{label}</div>
  </div>
);
