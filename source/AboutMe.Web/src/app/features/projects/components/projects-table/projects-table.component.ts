import { Component } from '@angular/core';

import { ProjectMetadataService } from '../../services/project-metadata.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.scss'],
})
export class ProjectsTableComponent {

  projects$ = this.projectService.getProjects();

  constructor(
    private router: Router,
    private projectService: ProjectMetadataService) {}

  routeToProject(link: string) {
    this.router.navigate([`/projects/${link}`]);
  }
}
