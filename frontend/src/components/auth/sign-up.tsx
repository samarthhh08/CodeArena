"use client";

import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { FiMail, FiLock, FiUser } from "react-icons/fi";
import { Link } from "react-router-dom";

const SignUp = () => {
  return (
    <Card className="w-[350px] max-h-[550px] rounded-2xl shadow-xl mt-5">
      <CardContent className=" px-4 py-2 sm:px-6 sm:py-3">
        {/* Logo */}
        <div className="w-[50px] h-[50px] rounded-full bg-blue-600 flex items-center justify-center mx-auto mb-4">
          <p className="font-bold text-white text-xl">{"</>"}</p>
        </div>

        {/* Title */}
        <h2 className="text-center text-2xl font-semibold text-gray-500 mb-8">
          Create your account
        </h2>

        {/* Username */}
        <div className="mb-4">
          <div className="flex items-center gap-3 rounded-xl border px-4 py-1  focus-within:ring-2 focus-within:ring-blue-400">
            <FiUser className="text-sm text-gray-500" />
            <Input
              type="text"
              placeholder="Username"
              className="border-0 focus-visible:ring-0 p-0 text-sm"
            />
          </div>
        </div>

        {/* Email */}
        <div className="mb-4">
          <div className="flex items-center gap-3 rounded-xl border px-4 py-1  focus-within:ring-2 focus-within:ring-blue-400">
            <FiMail className="text-sm text-gray-500" />
            <Input
              type="email"
              placeholder="Email"
              className="border-0 focus-visible:ring-0 p-0 text-sm"
            />
          </div>
        </div>

        {/* Password */}
        <div className="mb-2">
          <div className="flex items-center gap-3 rounded-xl border px-4 py-1 focus-within:ring-2 focus-within:ring-blue-400">
            <FiLock className="text-sm text-gray-500" />
            <Input
              type="password"
              placeholder="Password"
              className="border-0 focus-visible:ring-0 p-0 text-sm"
            />
          </div>
        </div>

        {/* Button */}
        <Button className="w-full rounded-full text-md py-6 bg-gradient-to-r from-blue-400 to-indigo-500 hover:opacity-90 my-6">
          Sign up
        </Button>

        {/* Footer */}
        <p className="text-center text-gray-400 mt-6">
          Already have an account?{" "}
          <Link
            to="/signin"
            className="text-blue-500 font-medium cursor-pointer hover:underline"
          >
            Sign In
          </Link>
        </p>
      </CardContent>
    </Card>
  );
};

export default SignUp;
