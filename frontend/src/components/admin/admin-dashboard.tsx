import { Link } from "react-router-dom";
import { Card } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { PlusCircle, List, Edit, LayoutDashboard, HelpCircle } from "lucide-react";

export default function AdminDashboard() {
  return (
    <div className="max-w-5xl mx-auto p-6 space-y-6">
      {/* Header */}
      <div className="flex items-center gap-3">
        <LayoutDashboard className="w-7 h-7 text-blue-600" />
        <h1 className="text-2xl font-bold">Admin Dashboard</h1>
      </div>

      <p className="text-muted-foreground">
        Manage problems, create new ones, and maintain the platform.
      </p>

      {/* Cards */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {/* Create Problem */}
        <DashboardCard
          title="Create Problem"
          description="Add a new coding problem with test cases and tags."
          icon={<PlusCircle className="w-6 h-6 text-green-600" />}
          to="/admin/create-problem"
          action="Create"
        />

        {/* Problem List */}
        <DashboardCard
          title="Problem List"
          description="View, edit, publish or unpublish existing problems."
          icon={<List className="w-6 h-6 text-blue-600" />}
          to="/admin/problems"
          action="View"
        />

        {/* Edit Problem (shortcut) */}
        <DashboardCard
          title="Edit Problem"
          description="Quickly edit an existing problem by selecting it."
          icon={<Edit className="w-6 h-6 text-orange-600" />}
          to="/admin/problems"
          action="Edit"
        />
      </div>

      <DashboardCard
        title="MCQ Questions"
        description="Manage multiple choice questions for student practice."
        icon={<HelpCircle className="w-6 h-6 text-purple-600" />}
        to="/admin/mcq"
        action="Manage"
      />
    </div>
  );
}

/* ---------------- Reusable Card ---------------- */

type DashboardCardProps = {
  title: string;
  description: string;
  icon: React.ReactNode;
  to: string;
  action: string;
};

function DashboardCard({
  title,
  description,
  icon,
  to,
  action,
}: DashboardCardProps) {
  return (
    <Card className="p-5 flex flex-col justify-between hover:shadow-md transition-shadow">
      <div className="space-y-3">
        <div className="flex items-center gap-3">
          {icon}
          <h2 className="font-semibold text-lg">{title}</h2>
        </div>
        <p className="text-sm text-muted-foreground">{description}</p>
      </div>

      <Link to={to} className="mt-4">
        <Button className="w-full">{action}</Button>
      </Link>
    </Card>
  );
}
