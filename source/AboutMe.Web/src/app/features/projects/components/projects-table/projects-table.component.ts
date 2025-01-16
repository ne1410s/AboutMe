import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { ProjectMetadataService } from '../../services/project-metadata.service';

@Component({
    selector: 'app-projects-table',
    templateUrl: './projects-table.component.html',
    styleUrls: ['./projects-table.component.scss'],
    standalone: false
})
export class ProjectsTableComponent {
  projects$ = this.projectService.getProjects();

  constructor(
    private router: Router,
    private projectService: ProjectMetadataService
  ) {}

  routeToProject(link: string) {
    this.router.navigate([`/projects/${link}`]);
  }
}
